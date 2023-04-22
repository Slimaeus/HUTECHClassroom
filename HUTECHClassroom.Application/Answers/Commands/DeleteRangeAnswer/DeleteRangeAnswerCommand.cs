using HUTECHClassroom.Application.Common.Requests;

namespace HUTECHClassroom.Application.Answers.Commands.DeleteRangeAnswer;

public record DeleteRangeAnswerCommand(IList<Guid> Ids) : DeleteRangeCommand(Ids);
public class DeleteRangeAnswerCommandHandler : DeleteRangeCommandHandler<Answer, DeleteRangeAnswerCommand>
{
    public DeleteRangeAnswerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}
