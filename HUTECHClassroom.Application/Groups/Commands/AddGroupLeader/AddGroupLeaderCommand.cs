using EntityFrameworkCore.Repository.Extensions;
using HUTECHClassroom.Domain.Constants;
using Microsoft.EntityFrameworkCore;

namespace HUTECHClassroom.Application.Groups.Commands.AddGroupLeader;
public record AddGroupLeaderCommand(Guid GroupId, Guid UserId) : IRequest<Unit>;
public class AddGroupLeaderCommandHandler : IRequestHandler<AddGroupLeaderCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IRepository<Group> _groupRepository;
    private readonly IRepository<GroupRole> _groupRoleRepository;
    private readonly IRepository<ApplicationUser> _userRepository;

    public AddGroupLeaderCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _groupRepository = _unitOfWork.Repository<Group>();
        _groupRoleRepository = _unitOfWork.Repository<GroupRole>();
        _userRepository = _unitOfWork.Repository<ApplicationUser>();
    }
    public async Task<Unit> Handle(AddGroupLeaderCommand request, CancellationToken cancellationToken)
    {
        var query = _groupRepository
            .SingleResultQuery()
            .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll)
            .Include(i => i.Include(x => x.GroupUsers).ThenInclude(x => x.User).Include(x => x.GroupUsers).ThenInclude(x => x.GroupRole))
            .AndFilter(x => x.Id == request.GroupId);

        var group = await _groupRepository
            .SingleOrDefaultAsync(query, cancellationToken);

        var leaderRole = await _groupRoleRepository.SingleOrDefaultAsync(_groupRoleRepository.SingleResultQuery().AndFilter(x => x.Name == GroupRoleConstants.LEADER), cancellationToken);
        var memberRole = await _groupRoleRepository.SingleOrDefaultAsync(_groupRoleRepository.SingleResultQuery().AndFilter(x => x.Name == GroupRoleConstants.MEMBER), cancellationToken);

        var currentLeaderGroupUser = group.GroupUsers.SingleOrDefault(x => x.GroupRole.Name == leaderRole.Name);

        if (currentLeaderGroupUser != null && currentLeaderGroupUser.UserId == request.UserId)
        {
            await _unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

            _groupRepository.RemoveTracking(group);

            return Unit.Value;
        }
        if (currentLeaderGroupUser != null)
            currentLeaderGroupUser.GroupRole = memberRole;

        var groupUserInGroup = group.GroupUsers.SingleOrDefault(x => x.UserId == request.UserId);

        if (groupUserInGroup == null)
        {
            var groupUser = _mapper.Map<GroupUser>(request);

            groupUser.GroupRole = leaderRole;

            group.GroupUsers.Add(groupUser);
        }
        else
        {
            groupUserInGroup.GroupRole = leaderRole;
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

        _groupRepository.RemoveTracking(group);

        return Unit.Value;
    }
}
