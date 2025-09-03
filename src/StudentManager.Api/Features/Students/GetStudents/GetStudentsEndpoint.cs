using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentManager.Api.Entities;

namespace StudentManager.Api.Features.Students.GetStudents
{
    public static class GetStudentsEndpoint
    {
        public record Query() : IRequest<IResult>;
        public class Handler : IRequestHandler<Query, IResult>
        {
            private readonly AppDbContext _context;
            public Handler(AppDbContext context)
            {
                _context = context;
            }
            public async Task<IResult> Handle(Query request, CancellationToken cancellationToken)
            {
                var students = await _context.Students.ToListAsync(cancellationToken);
                return Results.Ok(students);
            }
        }
        public class Endpoint : Carter.ICarterModule
        {
            public void AddRoutes(IEndpointRouteBuilder app)
            {
                app.MapGet("/api/students", async (IMediator mediator) =>
                {
                    var result = await mediator.Send(new Query());
                    return result;
                })
                .WithName("GetStudents")
                .Produces<List<Student>>(StatusCodes.Status200OK);
            }
        }
    }
}
