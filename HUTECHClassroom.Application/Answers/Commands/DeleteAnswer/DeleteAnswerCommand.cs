using HUTECHClassroom.Application.Answers.DTOs;
using HUTECHClassroom.Application.Common.Requests;

namespace HUTECHClassroom.Application.Answers.Commands.DeleteAnswer;

public record DeleteAnswerCommand(Guid Id) : DeleteCommand<AnswerDTO>(Id);
public class DeleteAnswerCommandHandler : DeleteCommandHandler<Answer, DeleteAnswerCommand, AnswerDTO>
{
    public DeleteAnswerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}
