using HUTECHClassroom.Application.Common.Requests;

namespace HUTECHClassroom.Application.Classs.Commands.DeleteRangeClass;

public record DeleteRangeClassCommand(IList<Guid> Ids) : DeleteRangeCommand(Ids);
public sealed class DeleteRangeClassCommandHandler : DeleteRangeCommandHandler<Class, DeleteRangeClassCommand>
{
    public DeleteRangeClassCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}
