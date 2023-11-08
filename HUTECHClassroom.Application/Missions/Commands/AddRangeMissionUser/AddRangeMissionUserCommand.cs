using EntityFrameworkCore.Repository.Extensions;
using Microsoft.EntityFrameworkCore;

namespace HUTECHClassroom.Application.Missions.Commands.AddRangeMissionUser;

public record AddRangeMissionUserCommand(Guid MissionId, IList<Guid> UserIds) : IRequest<Unit>;
public sealed class AddMissionUsersCommandHandler : IRequestHandler<AddRangeMissionUserCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<Mission> _repository;
    private readonly IRepository<ApplicationUser> _userRepository;

    public AddMissionUsersCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _repository = unitOfWork.Repository<Mission>();
        _userRepository = unitOfWork.Repository<ApplicationUser>();
    }
    public async Task<Unit> Handle(AddRangeMissionUserCommand request, CancellationToken cancellationToken)
    {
        var query = _repository
            .SingleResultQuery()
            .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll)
            .Include(i => i.Include(x => x.MissionUsers))
            .AndFilter(x => x.Id == request.MissionId);

        var mission = await _repository
            .SingleOrDefaultAsync(query, cancellationToken);

        var missionUserIds = mission.MissionUsers.Select(mu => mu.UserId);

        var userQuery = _userRepository
            .MultipleResultQuery()
            .AndFilter(x => !missionUserIds.Contains(x.Id))
            .AndFilter(x => request.UserIds.Contains(x.Id));

        var users = await _userRepository
            .SearchAsync(userQuery, cancellationToken);

        if (users.Count <= 0) return Unit.Value;

        foreach (var user in users)
        {
            mission.MissionUsers.Add(new MissionUser { UserId = user.Id });
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

        _repository.RemoveTracking(mission);

        return Unit.Value;
    }
}
