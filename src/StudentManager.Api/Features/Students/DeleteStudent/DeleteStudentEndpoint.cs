using FluentValidation;
using MediatR;

namespace StudentManager.Api.Features.Students.DeleteStudent
{
    public static class DeleteStudentEndpoint
    {
        public record Command(Guid Id) : IRequest<IResult>;
        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(x => x.Id)
                    .NotEmpty().WithMessage("Id is required.");
            }
        }
        public class Handler : IRequestHandler<Command, IResult>
        {
            private readonly AppDbContext _context;
            private readonly IValidator<Command> _validator;
            public Handler(AppDbContext context, IValidator<Command> validator)
            {
                _context = context;
                _validator = validator;
            }
            public async Task<IResult> Handle(Command request, CancellationToken cancellationToken)
            {
                var validationResult = await _validator.ValidateAsync(request, cancellationToken);
                if (!validationResult.IsValid)
                    return Results.BadRequest(validationResult.Errors);
                var student = await _context.Students.FindAsync(new object[] { request.Id }, cancellationToken);
                if (student == null)
                    return Results.NotFound($"Student with Id {request.Id} not found.");
                _context.Students.Remove(student);
                await _context.SaveChangesAsync(cancellationToken);
                return Results.NoContent();
            }
        }
        public class Endpoint : Carter.ICarterModule
        {
            public void AddRoutes(IEndpointRouteBuilder app)
            {
                app.MapDelete("/api/students/{id:guid}", async (IMediator mediator, Guid id) =>
                {
                    var result = await mediator.Send(new Command(id));
                    return result;
                })
                .WithName("DeleteStudent")
                .Produces(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status404NotFound);
            }
        }
    }
}
