using HUTECHClassroom.Application.Common.Requests;

namespace HUTECHClassroom.Application.Exercises.Commands.CreateExercise;

public sealed record CreateExerciseCommand : CreateCommand
{
    public required string Title { get; set; }
    public string? Instruction { get; set; }
    public string? Link { get; set; }
    public float TotalScore { get; set; }
    public DateTime Deadline { get; set; }
    public string? Topic { get; set; }
    public string? Criteria { get; set; }
    public Guid ClassroomId { get; set; }
}
public sealed class CreateExerciseCommandHandler : CreateCommandHandler<Exercise, CreateExerciseCommand>
{

    public CreateExerciseCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }
}
