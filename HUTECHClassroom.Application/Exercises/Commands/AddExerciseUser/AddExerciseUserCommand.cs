using EntityFrameworkCore.Repository.Extensions;
using Microsoft.EntityFrameworkCore;

namespace HUTECHClassroom.Application.Exercises.Commands.AddExerciseUser;

public record AddExerciseUserCommand(Guid ExerciseId, Guid UserId) : IRequest<Unit>;
public class AddExerciseUserCommandHandler : IRequestHandler<AddExerciseUserCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IRepository<Exercise> _repository;
    private readonly IRepository<ApplicationUser> _userRepository;

    public AddExerciseUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _repository = unitOfWork.Repository<Exercise>();
        _userRepository = _unitOfWork.Repository<ApplicationUser>();
    }
    public async Task<Unit> Handle(AddExerciseUserCommand request, CancellationToken cancellationToken)
    {
        var query = _repository
            .SingleResultQuery()
            .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll)
            .Include(i => i.Include(x => x.ExerciseUsers))
            .AndFilter(x => x.Id == request.ExerciseId);

        var exercise = await _repository
            .SingleOrDefaultAsync(query, cancellationToken);

        if (exercise.ExerciseUsers.Any(x => x.UserId == request.UserId))
            throw new InvalidOperationException($"{request.UserId} already exists");

        var exerciseUser = _mapper.Map<ExerciseUser>(request);

        exercise.ExerciseUsers.Add(exerciseUser);

        await _unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

        _repository.RemoveTracking(exercise);

        return Unit.Value;
    }
}
