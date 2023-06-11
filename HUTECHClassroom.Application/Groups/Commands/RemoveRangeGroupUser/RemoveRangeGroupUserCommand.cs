using EntityFrameworkCore.Repository.Extensions;
using Microsoft.EntityFrameworkCore;

namespace HUTECHClassroom.Application.Groups.Commands.RemoveRangeGroupUser;

public record RemoveRangeGroupUserCommand(Guid GroupId, IList<Guid> UserIds) : IRequest<Unit>;
public class RemoveGroupUsersCommandHandler : IRequestHandler<RemoveRangeGroupUserCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<Group> _repository;

    public RemoveGroupUsersCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _repository = unitOfWork.Repository<Group>();
    }
    public async Task<Unit> Handle(RemoveRangeGroupUserCommand request, CancellationToken cancellationToken)
    {
        var query = _repository
            .SingleResultQuery()
            .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll)
            .Include(i => i.Include(x => x.GroupUsers))
            .AndFilter(x => x.Id == request.GroupId);

        var group = await _repository
            .SingleOrDefaultAsync(query, cancellationToken);

        var users = group.GroupUsers
            .Where(x => request.UserIds.Contains(x.UserId)).ToList();

        foreach (var user in users)
        {
            group.GroupUsers.Remove(user);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

        _repository.RemoveTracking(group);

        return Unit.Value;
    }
}
