using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Domain.Enums;

namespace HUTECHClassroom.Application.Classrooms.Commands.UpdateClassroom;

public record UpdateClassroomCommand(Guid Id) : UpdateCommand(Id)
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Topic { get; set; }
    public string Room { get; set; }
    public string StudyPeriod { get; set; }
    public string Class { get; set; }
    public string SchoolYear { get; set; }
    public string StudyGroup { get; set; }
    public string PracticalStudyGroup { get; set; }
    public Semester Semester { get; set; } = Semester.I;
    public ClassroomType Type { get; set; } = ClassroomType.TheoryRoom;
};
public class UpdateClassroomCommandHandler : UpdateCommandHandler<Classroom, UpdateClassroomCommand>
{
    public UpdateClassroomCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}
