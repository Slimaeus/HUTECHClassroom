using EntityFrameworkCore.Repository.Extensions;
using Microsoft.EntityFrameworkCore;

namespace HUTECHClassroom.Application.Missions.Commands.RemoveRangeMissionUser;

public record RemoveRangeMissionUserCommand(Guid MissionId, IList<Guid> UserIds) : IRequest<Unit>;
public class RemoveMissionUsersCommandHandler : IRequestHandler<RemoveRangeMissionUserCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<Mission> _repository;

    public RemoveMissionUsersCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _repository = unitOfWork.Repository<Mission>();
    }
    public async Task<Unit> Handle(RemoveRangeMissionUserCommand request, CancellationToken cancellationToken)
    {
        var query = _repository
            .SingleResultQuery()
            .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll)
            .Include(i => i.Include(x => x.MissionUsers))
            .AndFilter(x => x.Id == request.MissionId);

        var mission = await _repository
            .SingleOrDefaultAsync(query, cancellationToken);

        var users = mission.MissionUsers
            .Where(x => request.UserIds.Contains(x.UserId)).ToList();

        foreach (var user in users)
        {
            mission.MissionUsers.Remove(user);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

        _repository.RemoveTracking(mission);

        return Unit.Value;
    }
}
