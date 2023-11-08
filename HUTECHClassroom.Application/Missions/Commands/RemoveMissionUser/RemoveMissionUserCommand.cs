using EntityFrameworkCore.Repository.Extensions;
using Microsoft.EntityFrameworkCore;

namespace HUTECHClassroom.Application.Missions.Commands.RemoveMissionUser;

public record RemoveMissionUserCommand(Guid MissionId, Guid UserId) : IRequest<Unit>;
public sealed class RemoveMissionUserCommandHandler : IRequestHandler<RemoveMissionUserCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<Mission> _repository;

    public RemoveMissionUserCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _repository = unitOfWork.Repository<Mission>();
    }
    public async Task<Unit> Handle(RemoveMissionUserCommand request, CancellationToken cancellationToken)
    {
        var query = _repository
            .SingleResultQuery()
            .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll)
            .Include(i => i.Include(x => x.MissionUsers).ThenInclude(x => x.User))
            .AndFilter(x => x.Id == request.MissionId);

        var mission = await _repository
            .SingleOrDefaultAsync(query, cancellationToken);

        var user = mission.MissionUsers.SingleOrDefault(x => x.UserId == request.UserId);

        if (user == null) throw new InvalidOperationException($"User {request.UserId} is not in this mission");

        mission.MissionUsers.Remove(user);

        await _unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

        _repository.RemoveTracking(mission);

        return Unit.Value;
    }
}
