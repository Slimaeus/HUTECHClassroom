using EntityFrameworkCore.Repository.Extensions;
using HUTECHClassroom.Domain.Constants;
using Microsoft.EntityFrameworkCore;

namespace HUTECHClassroom.Application.Groups.Commands.AddRangeGroupUser;

public record AddRangeGroupUserCommand(Guid GroupId, IList<Guid> UserIds) : IRequest<Unit>;
public sealed class AddGroupUsersCommandHandler : IRequestHandler<AddRangeGroupUserCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<Group> _repository;
    private readonly IRepository<ApplicationUser> _userRepository;
    private readonly IRepository<GroupRole> _groupRoleRepository;

    public AddGroupUsersCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _repository = unitOfWork.Repository<Group>();
        _userRepository = unitOfWork.Repository<ApplicationUser>();
        _groupRoleRepository = _unitOfWork.Repository<GroupRole>();
    }
    public async Task<Unit> Handle(AddRangeGroupUserCommand request, CancellationToken cancellationToken)
    {
        var query = _repository
            .SingleResultQuery()
            .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll)
            .Include(i => i.Include(x => x.GroupUsers))
            .AndFilter(x => x.Id == request.GroupId);

        var group = await _repository
            .SingleOrDefaultAsync(query, cancellationToken);

        var groupUserIds = group.GroupUsers.Select(gu => gu.UserId);

        var userQuery = _userRepository
            .MultipleResultQuery()
            .AndFilter(x => !groupUserIds.Contains(x.Id))
            .AndFilter(x => request.UserIds.Contains(x.Id));

        var users = await _userRepository
            .SearchAsync(userQuery, cancellationToken);

        if (users.Count <= 0) return Unit.Value;

        var groupRoleQuery = _groupRoleRepository
            .SingleResultQuery()
            .AndFilter(x => x.Name == GroupRoleConstants.MEMBER);

        var groupRole = await _groupRoleRepository.SingleOrDefaultAsync(groupRoleQuery, cancellationToken);

        foreach (var user in users)
        {
            group.GroupUsers.Add(new GroupUser { UserId = user.Id, GroupRoleId = groupRole.Id });
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

        _repository.RemoveTracking(group);

        return Unit.Value;
    }
}
