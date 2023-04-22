using HUTECHClassroom.Application.Common.Requests;

namespace HUTECHClassroom.Application.Exercises.Commands.DeleteRangeExercise;

public record DeleteRangeExerciseCommand(IList<Guid> Ids) : DeleteRangeCommand(Ids);
public class DeleteRangeExerciseCommandHandler : DeleteRangeCommandHandler<Exercise, DeleteRangeExerciseCommand>
{
    public DeleteRangeExerciseCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}
