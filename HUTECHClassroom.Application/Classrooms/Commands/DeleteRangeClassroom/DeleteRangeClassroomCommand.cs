using HUTECHClassroom.Application.Common.Requests;

namespace HUTECHClassroom.Application.Classrooms.Commands.DeleteRangeClassroom;

public record DeleteRangeClassroomCommand(IList<Guid> Ids) : DeleteRangeCommand(Ids);
public sealed class DeleteRangeClassroomCommandHandler : DeleteRangeCommandHandler<Classroom, DeleteRangeClassroomCommand>
{
    public DeleteRangeClassroomCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}
