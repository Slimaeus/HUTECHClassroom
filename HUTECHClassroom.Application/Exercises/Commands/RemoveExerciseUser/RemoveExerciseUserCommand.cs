using EntityFrameworkCore.Repository.Extensions;
using Microsoft.EntityFrameworkCore;

namespace HUTECHClassroom.Application.Exercises.Commands.RemoveExerciseUser;

public record RemoveExerciseUserCommand(Guid ExerciseId, Guid UserId) : IRequest<Unit>;
public class RemoveExerciseUserCommandHandler : IRequestHandler<RemoveExerciseUserCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<Exercise> _repository;

    public RemoveExerciseUserCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _repository = unitOfWork.Repository<Exercise>();
    }
    public async Task<Unit> Handle(RemoveExerciseUserCommand request, CancellationToken cancellationToken)
    {
        var query = _repository
            .SingleResultQuery()
            .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll)
            .Include(i => i.Include(x => x.ExerciseUsers).ThenInclude(x => x.User))
            .AndFilter(x => x.Id == request.ExerciseId);

        var exercise = await _repository
            .SingleOrDefaultAsync(query, cancellationToken);

        var user = exercise.ExerciseUsers.SingleOrDefault(x => x.UserId == request.UserId)
            ?? throw new InvalidOperationException($"User {request.UserId} is not in take this exercise");
        exercise.ExerciseUsers.Remove(user);

        await _unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

        _repository.RemoveTracking(exercise);

        return Unit.Value;
    }
}
