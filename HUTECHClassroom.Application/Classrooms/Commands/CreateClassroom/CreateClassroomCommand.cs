using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Domain.Enums;

namespace HUTECHClassroom.Application.Classrooms.Commands.CreateClassroom;

public record CreateClassroomCommand : CreateCommand
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Topic { get; set; }
    public string Room { get; set; }
    public string StudyPeriod { get; set; }
    public string SchoolYear { get; set; }
    public string StudyGroup { get; set; }
    public string PracticalStudyGroup { get; set; }
    public Semester Semester { get; set; } = Semester.I;
    public ClassroomType Type { get; set; } = ClassroomType.TheoryRoom;
    public Guid? FacultyId { get; set; }
    public Guid LecturerId { get; set; }
    public Guid? SubjectId { get; set; }
    public Guid? ClassId { get; set; }
}
public class CreateClassroomCommandHandler : CreateCommandHandler<Classroom, CreateClassroomCommand>
{

    public CreateClassroomCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}
