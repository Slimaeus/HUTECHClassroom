using HUTECHClassroom.Application.Common.Requests;

namespace HUTECHClassroom.Application.Subjects.Commands.DeleteRangeSubject;

public record DeleteRangeSubjectCommand(IList<Guid> Ids) : DeleteRangeCommand(Ids);
public class DeleteRangeSubjectCommandHandler : DeleteRangeCommandHandler<Subject, DeleteRangeSubjectCommand>
{
    public DeleteRangeSubjectCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}
