using EntityFrameworkCore.Repository.Extensions;
using Microsoft.EntityFrameworkCore;

namespace HUTECHClassroom.Application.Groups.Commands.RemoveGroupUser;

public record RemoveGroupUserCommand(Guid GroupId, Guid UserId) : IRequest<Unit>;
public sealed class RemoveGroupUserCommandHandler : IRequestHandler<RemoveGroupUserCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<Group> _repository;

    public RemoveGroupUserCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _repository = unitOfWork.Repository<Group>();
    }
    public async Task<Unit> Handle(RemoveGroupUserCommand request, CancellationToken cancellationToken)
    {
        var query = _repository
            .SingleResultQuery()
            .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll)
            .Include(i => i.Include(x => x.GroupUsers).ThenInclude(x => x.User))
            .AndFilter(x => x.Id == request.GroupId);

        var group = await _repository
            .SingleOrDefaultAsync(query, cancellationToken);

        var user = group.GroupUsers.SingleOrDefault(x => x.UserId == request.UserId);

        if (user == null) throw new InvalidOperationException($"User {request.UserId} is not in this group");

        group.GroupUsers.Remove(user);

        await _unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

        _repository.RemoveTracking(group);

        return Unit.Value;
    }
}
