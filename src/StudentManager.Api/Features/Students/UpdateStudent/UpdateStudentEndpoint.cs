using FluentValidation;
using MediatR;

namespace StudentManager.Api.Features.Students.UpdateStudent
{
    public static class UpdateStudentEndpoint
    {
        public class Validator : AbstractValidator<UpdateStudentRequest>
        {
            public Validator()
            {
                RuleFor(x => x.Id)
                    .NotEmpty().WithMessage("Id is required.");
                RuleFor(x => x.Name)
                    .NotEmpty().WithMessage("Name is required.")
                    .MaximumLength(100).WithMessage("Name must be at most 100 characters.");
                RuleFor(x => x.Age)
                    .GreaterThan(0).WithMessage("Age must be greater than zero.");
            }
        }

        public class Handler : IRequestHandler<UpdateStudentRequest, IResult>
        {
            private readonly AppDbContext _context;
            private readonly IValidator<UpdateStudentRequest> _validator;

            public Handler(AppDbContext context, IValidator<UpdateStudentRequest> validator)
            {
                _context = context;
                _validator = validator;
            }

            public async Task<IResult> Handle(UpdateStudentRequest request, CancellationToken cancellationToken)
            {
                var validationResult = await _validator.ValidateAsync(request, cancellationToken);
                if (!validationResult.IsValid)
                    return Results.BadRequest(validationResult.Errors);

                var student = await _context.Students.FindAsync(new object[] { request.Id }, cancellationToken);
                if (student == null)
                    return Results.NotFound($"Student with Id {request.Id} not found.");

                student.Name = request.Name;
                student.Age = request.Age;

                _context.Students.Update(student);
                await _context.SaveChangesAsync(cancellationToken);

                return Results.NoContent();
            }
        }

        public class Endpoint : Carter.ICarterModule
        {
            public void AddRoutes(IEndpointRouteBuilder app)
            {
                app.MapPut("/api/students/update", async (IMediator mediator, UpdateStudentRequest cmd) =>
                {
                    await mediator.Send(cmd);
                    return Results.NoContent();
                })
                .WithName("UpdateStudent")
                .Produces(StatusCodes.Status204NoContent)
                .ProducesValidationProblem()
                .WithTags("Students");
            }
        }
    }
}
