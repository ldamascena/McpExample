using MediatR;

namespace StudentManager.Api.Features.Students.UpdateStudent
{
    public class UpdateStudentRequest : IRequest<IResult>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
