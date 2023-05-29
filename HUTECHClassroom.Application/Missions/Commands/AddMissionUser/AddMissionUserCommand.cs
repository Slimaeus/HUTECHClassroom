using EntityFrameworkCore.Repository.Extensions;
using HUTECHClassroom.Application.Common.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace HUTECHClassroom.Application.Missions.Commands.AddMissionUser;

public record AddMissionUserCommand(Guid Id, Guid UserId) : IRequest<Unit>;
public class AddMissionUserCommandHandler : IRequestHandler<AddMissionUserCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<Mission> _repository;
    private readonly IRepository<ApplicationUser> _userRepository;

    public AddMissionUserCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _repository = unitOfWork.Repository<Mission>();
        _userRepository = unitOfWork.Repository<ApplicationUser>();
    }
    public async Task<Unit> Handle(AddMissionUserCommand request, CancellationToken cancellationToken)
    {
        var query = _repository
            .SingleResultQuery()
            .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll)
            .Include(i => i.Include(x => x.MissionUsers).ThenInclude(x => x.User))
            .AndFilter(x => x.Id == request.Id);

        var mission = await _repository
            .SingleOrDefaultAsync(query, cancellationToken);

        if (mission == null) throw new NotFoundException(nameof(Mission), request.Id);

        if (mission.MissionUsers.Any(x => x.UserId == request.UserId)) throw new InvalidOperationException($"{request.UserId} already exists");

        var userQuery = _userRepository
            .SingleResultQuery()
            .AndFilter(x => x.Id == request.UserId);

        var user = await _userRepository
            .SingleOrDefaultAsync(userQuery, cancellationToken);

        if (user == null) throw new NotFoundException(nameof(ApplicationUser), request.UserId);

        mission.MissionUsers.Add(new MissionUser { User = user });

        await _unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

        _repository.RemoveTracking(mission);

        return Unit.Value;
    }
}
