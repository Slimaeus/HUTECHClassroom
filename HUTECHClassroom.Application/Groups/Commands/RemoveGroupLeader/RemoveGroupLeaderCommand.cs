using EntityFrameworkCore.Repository.Extensions;
using HUTECHClassroom.Application.Common.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace HUTECHClassroom.Application.Groups.Commands.RemoveGroupLeader;
public record RemoveGroupLeaderCommand(Guid Id, Guid UserId) : IRequest<Unit>;
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

        var memberRole = await _groupRoleRepository.SingleOrDefaultAsync(_groupRoleRepository.SingleResultQuery().AndFilter(x => x.Name == "Member"), cancellationToken);

        var groupUser = group.GroupUsers.SingleOrDefault(x => x.UserId == user.Id && x.GroupRole.Name == "Leader");

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