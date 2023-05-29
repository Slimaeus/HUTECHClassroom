using EntityFrameworkCore.Repository.Extensions;
using HUTECHClassroom.Application.Common.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace HUTECHClassroom.Application.Groups.Commands.AddGroupLeader;
public record AddGroupLeaderCommand(Guid Id, Guid UserId) : IRequest<Unit>;
public class AddGroupLeaderCommandHandler : IRequestHandler<AddGroupLeaderCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<Group> _groupRepository;
    private readonly IRepository<GroupRole> _groupRoleRepository;
    private readonly IRepository<ApplicationUser> _userRepository;

    public AddGroupLeaderCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
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
            .AndFilter(x => x.Id == request.Id);

        var group = await _groupRepository
            .SingleOrDefaultAsync(query, cancellationToken);

        if (group == null) throw new NotFoundException(nameof(Group), request.Id);

        var userQuery = _userRepository
            .SingleResultQuery()
            .AndFilter(x => x.Id == request.UserId);

        var user = await _userRepository
            .SingleOrDefaultAsync(userQuery, cancellationToken);

        if (user == null) throw new NotFoundException(nameof(ApplicationUser), request.UserId);

        var leaderRole = await _groupRoleRepository.SingleOrDefaultAsync(_groupRoleRepository.SingleResultQuery().AndFilter(x => x.Name == "Leader"), cancellationToken);
        var memberRole = await _groupRoleRepository.SingleOrDefaultAsync(_groupRoleRepository.SingleResultQuery().AndFilter(x => x.Name == "Member"), cancellationToken);

        var currentLeaderGroupUser = group.GroupUsers.SingleOrDefault(x => x.GroupRole.Name == leaderRole.Name);

        if (currentLeaderGroupUser != null && currentLeaderGroupUser.UserId == user.Id)
        {
            await _unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

            _groupRepository.RemoveTracking(group);

            return Unit.Value;
        }
        if (currentLeaderGroupUser != null)
            currentLeaderGroupUser.GroupRole = memberRole;

        var groupUser = group.GroupUsers.SingleOrDefault(x => x.UserId == user.Id);

        if (groupUser == null)
        {
            group.GroupUsers.Add(new GroupUser
            {
                User = user,
                GroupRole = leaderRole
            });
        }
        else
        {
            groupUser.GroupRole = leaderRole;
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

        _groupRepository.RemoveTracking(group);

        return Unit.Value;
    }
}
