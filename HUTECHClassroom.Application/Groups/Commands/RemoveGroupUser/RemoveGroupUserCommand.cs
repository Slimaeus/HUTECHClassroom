using EntityFrameworkCore.Repository.Extensions;
using EntityFrameworkCore.Repository.Interfaces;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using HUTECHClassroom.Application.Common.Exceptions;
using HUTECHClassroom.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HUTECHClassroom.Application.Groups.Commands.RemoveGroupUser;

public record RemoveGroupUserCommand(Guid Id, string UserName) : IRequest<Unit>;
public class RemoveGroupUserCommandHandler : IRequestHandler<RemoveGroupUserCommand, Unit>
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
            .AndFilter(x => x.Id == request.Id);

        var group = await _repository
            .SingleOrDefaultAsync(query, cancellationToken);

        if (group == null) throw new NotFoundException(nameof(Group), request.Id);

        var user = group.GroupUsers.SingleOrDefault(x => x.User.UserName == request.UserName);

        if (user == null) throw new NotFoundException(nameof(ApplicationUser), request.UserName);

        group.GroupUsers.Remove(user);

        await _unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

        _repository.RemoveTracking(group);

        return Unit.Value;
    }
}
