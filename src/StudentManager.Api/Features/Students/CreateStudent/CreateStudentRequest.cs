using MediatR;

namespace StudentManager.Api.Features.Students.CreateStudent
{
    public class CreateStudentRequest : IRequest<IResult>
    {
        public string Name { get; set; } 
        public int Age { get; set; }
    }
}
