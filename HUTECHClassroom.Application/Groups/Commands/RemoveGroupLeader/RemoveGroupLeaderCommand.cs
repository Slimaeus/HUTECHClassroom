using EntityFrameworkCore.Repository.Extensions;
using HUTECHClassroom.Domain.Constants;
using Microsoft.EntityFrameworkCore;

namespace HUTECHClassroom.Application.Groups.Commands.RemoveGroupLeader;
public record RemoveGroupLeaderCommand(Guid GroupId, Guid UserId) : IRequest<Unit>;
public class RemoveGroupLeaderCommandHandler : IRequestHandler<RemoveGroupLeaderCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<Group> _groupRepository;
    private readonly IRepository<GroupRole> _groupRoleRepository;
    private readonly IRepository<ApplicationUser> _userRepository;

    public RemoveGroupLeaderCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _groupRepository = _unitOfWork.Repository<Group>();
        _groupRoleRepository = _unitOfWork.Repository<GroupRole>();
        _userRepository = _unitOfWork.Repository<ApplicationUser>();
    }
    public async Task<Unit> Handle(RemoveGroupLeaderCommand request, CancellationToken cancellationToken)
    {
        var query = _groupRepository
            .SingleResultQuery()
            .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll)
            .Include(i => i.Include(x => x.GroupUsers).ThenInclude(x => x.User).Include(x => x.GroupUsers).ThenInclude(x => x.GroupRole))
            .AndFilter(x => x.Id == request.GroupId);

        var group = await _groupRepository
            .SingleOrDefaultAsync(query, cancellationToken);

        var memberRole = await _groupRoleRepository.SingleOrDefaultAsync(
            _groupRoleRepository
                .SingleResultQuery()
                .AndFilter(x => x.Name == GroupRoleConstants.MEMBER), cancellationToken);

        var groupUser = group.GroupUsers.SingleOrDefault(x => x.UserId == request.UserId && x.GroupRole.Name == GroupRoleConstants.LEADER);

        if (groupUser != null)
        {
            groupUser.GroupRole = memberRole;

            group.Leader = null;

            await _unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        _groupRepository.RemoveTracking(group);

        return Unit.Value;
    }
}