using HUTECHClassroom.Application.Common.Requests;

namespace HUTECHClassroom.Application.Groups.Commands.DeleteRangeGroup;

public record DeleteRangeGroupCommand(IList<Guid> Ids) : DeleteRangeCommand(Ids);
public sealed class DeleteRangeGroupCommandHandler : DeleteRangeCommandHandler<Group, DeleteRangeGroupCommand>
{
    public DeleteRangeGroupCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}
