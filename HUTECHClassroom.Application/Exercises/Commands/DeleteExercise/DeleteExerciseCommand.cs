using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Exercises.DTOs;

namespace HUTECHClassroom.Application.Exercises.Commands.DeleteExercise;

public record DeleteExerciseCommand(Guid Id) : DeleteCommand<ExerciseDTO>(Id);
public class DeleteExerciseCommandHandler : DeleteCommandHandler<Exercise, DeleteExerciseCommand, ExerciseDTO>
{
    public DeleteExerciseCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}
