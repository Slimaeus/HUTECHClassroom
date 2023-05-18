using HUTECHClassroom.Application.Common.Requests;

namespace HUTECHClassroom.Application.Subjects.Commands.DeleteRangeSubject;

public record DeleteRangeSubjectCommand(IList<string> Ids) : DeleteRangeCommand<string>(Ids);
public class DeleteRangeSubjectCommandHandler : DeleteRangeCommandHandler<string, Subject, DeleteRangeSubjectCommand>
{
    public DeleteRangeSubjectCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}
