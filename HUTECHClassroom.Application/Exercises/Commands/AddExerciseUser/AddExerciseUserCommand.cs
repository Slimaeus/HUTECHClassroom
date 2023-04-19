using EntityFrameworkCore.Repository.Extensions;
using EntityFrameworkCore.Repository.Interfaces;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using HUTECHClassroom.Application.Common.Exceptions;
using HUTECHClassroom.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HUTECHClassroom.Application.Exercises.Commands.AddExerciseUser;

public record AddExerciseUserCommand(Guid Id, string UserName) : IRequest<Unit>;
public class AddExerciseUserCommandHandler : IRequestHandler<AddExerciseUserCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<Exercise> _repository;
    private readonly IRepository<ApplicationUser> _userRepository;

    public AddExerciseUserCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _repository = unitOfWork.Repository<Exercise>();
        _userRepository = _unitOfWork.Repository<ApplicationUser>();
    }
    public async Task<Unit> Handle(AddExerciseUserCommand request, CancellationToken cancellationToken)
    {
        var query = _repository
            .SingleResultQuery()
            .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll)
            .Include(i => i.Include(x => x.ExerciseUsers).ThenInclude(x => x.User))
            .AndFilter(x => x.Id == request.Id);

        var exercise = await _repository
            .SingleOrDefaultAsync(query, cancellationToken);

        if (exercise == null) throw new NotFoundException(nameof(Exercise), request.Id);

        if (exercise.ExerciseUsers.Any(x => x.User.UserName == request.UserName)) throw new InvalidOperationException($"{request.UserName} already exists");

        var userQuery = _userRepository
            .SingleResultQuery()
            .AndFilter(x => x.UserName == request.UserName);

        var user = await _userRepository
            .SingleOrDefaultAsync(userQuery, cancellationToken);

        if (user == null) throw new NotFoundException(nameof(ApplicationUser), request.UserName);

        exercise.ExerciseUsers.Add(new ExerciseUser { User = user });

        await _unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

        _repository.RemoveTracking(exercise);

        return Unit.Value;
    }
}
