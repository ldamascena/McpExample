using FluentValidation;
using MediatR;
using StudentManager.Api.Entities;

namespace StudentManager.Api.Features.Students.GetStudentById
{
    public static class GetStudentByIdEndpoint
    {
        public record Query(Guid Id) : IRequest<IResult>;
        public class Validator : AbstractValidator<Query>
        {
            public Validator()
            {
                RuleFor(x => x.Id)
                    .NotEmpty().WithMessage("Id is required.");
            }
        }
        public class Handler : IRequestHandler<Query, IResult>
        {
            private readonly AppDbContext _context;
            private readonly IValidator<Query> _validator;
            public Handler(AppDbContext context, IValidator<Query> validator)
            {
                _context = context;
                _validator = validator;
            }
            public async Task<IResult> Handle(Query request, CancellationToken cancellationToken)
            {
                var validationResult = await _validator.ValidateAsync(request, cancellationToken);
                if (!validationResult.IsValid)
                    return Results.BadRequest(validationResult.Errors);
                var student = await _context.Students.FindAsync(new object[] { request.Id }, cancellationToken);
                if (student == null)
                    return Results.NotFound($"Student with Id {request.Id} not found.");
                return Results.Ok(student);
            }
        }
        public class Endpoint : Carter.ICarterModule
        {
            public void AddRoutes(IEndpointRouteBuilder app)
            {
                app.MapGet("/api/students/{id:guid}", async (IMediator mediator, Guid id) =>
                {
                    var result = await mediator.Send(new Query(id));
                    return result;
                })
                .WithName("GetStudent")
                .Produces<Student>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status404NotFound);
            }
        }
    }
}
