using HUTECHClassroom.Application.Common.Requests;

namespace HUTECHClassroom.Application.Classrooms.Commands.UpdateClassroom;

public record UpdateClassroomCommand(Guid Id) : UpdateCommand(Id)
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Topic { get; set; }
};
public class UpdateClassroomCommandHandler : UpdateCommandHandler<Classroom, UpdateClassroomCommand>
{
    public UpdateClassroomCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}
