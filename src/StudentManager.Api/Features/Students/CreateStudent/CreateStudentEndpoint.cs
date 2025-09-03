using Carter;
using FluentValidation;
using MediatR;
using StudentManager.Api.Entities;
using System;

namespace StudentManager.Api.Features.Students.CreateStudent
{
    public static class CreateStudent
    {
        public class Validator : AbstractValidator<CreateStudentRequest>
        {
            public Validator()
            {
                RuleFor(x => x.Name)
                    .NotEmpty().WithMessage("O nome é obrigatório.")
                    .MaximumLength(100).WithMessage("O nome deve ter no máximo 100 caracteres.");
                RuleFor(x => x.Age)
                    .GreaterThan(0).WithMessage("A idade deve ser maior que zero.");
            }
        }

        public class Handler : IRequestHandler<CreateStudentRequest, IResult>
        {
            private readonly AppDbContext _context;
            private readonly IValidator<CreateStudentRequest> _validator;

            public Handler(AppDbContext context, IValidator<CreateStudentRequest> validator)
            {
                _context = context;
                _validator = validator;
            }

            public async Task<IResult> Handle(CreateStudentRequest request, CancellationToken cancellationToken)
            {
                var validationResult = await _validator.ValidateAsync(request, cancellationToken);
                if (!validationResult.IsValid)
                    return Results.BadRequest(validationResult.Errors);

                var student = new Student
                {
                    Name = request.Name,
                    Age = request.Age
                };

                await _context.Students.AddAsync(student);
                await _context.SaveChangesAsync(cancellationToken);

                return Results.Ok(student.Id);
            }
        }

        public class Endpoint : ICarterModule
        {
            public void AddRoutes(IEndpointRouteBuilder app)
            {
                app.MapPost("/api/students/create", async (IMediator mediator, CreateStudentRequest cmd) =>
                {
                    return await mediator.Send(cmd);
                })
                .WithName("CreateStudent")
                .Produces<Guid>(StatusCodes.Status200OK)
                .Produces<IEnumerable<FluentValidation.Results.ValidationFailure>>(StatusCodes.Status400BadRequest)
                .WithTags("Students");
            }
        }
    }


}
