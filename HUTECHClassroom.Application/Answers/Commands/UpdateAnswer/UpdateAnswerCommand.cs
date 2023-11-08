using HUTECHClassroom.Application.Common.Requests;

namespace HUTECHClassroom.Application.Answers.Commands.UpdateAnswer;

public sealed record UpdateAnswerCommand(Guid Id) : UpdateCommand(Id)
{
    public string? Description { get; set; }
    public string? Link { get; set; }
    public float? Score { get; set; }
}
public sealed class UpdateAnswerCommandHandler : UpdateCommandHandler<Answer, UpdateAnswerCommand>
{
    public UpdateAnswerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}
