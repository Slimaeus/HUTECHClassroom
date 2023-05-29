using HUTECHClassroom.Application.Common.Requests;

namespace HUTECHClassroom.Application.Answers.Commands.CreateAnswer;

public record CreateAnswerCommand : CreateCommand
{
    public string Description { get; set; }
    public string Link { get; set; }
    public float Score { get; set; }
    public Guid UserId { get; set; }
    public Guid ExerciseId { get; set; }
}
public class CreateAnswerCommandHandler : CreateCommandHandler<Answer, CreateAnswerCommand>
{
    public CreateAnswerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}
