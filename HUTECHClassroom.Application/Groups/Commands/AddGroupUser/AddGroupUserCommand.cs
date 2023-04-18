using EntityFrameworkCore.Repository.Extensions;
using EntityFrameworkCore.Repository.Interfaces;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using HUTECHClassroom.Application.Common.Exceptions;
using HUTECHClassroom.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HUTECHClassroom.Application.Groups.Commands.AddGroupUser;

public record AddGroupUserCommand(Guid Id, string UserName) : IRequest<Unit>;
public class AddGroupUserCommandHandler : IRequestHandler<AddGroupUserCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<Group> _repository;
    private readonly IRepository<ApplicationUser> _userRepository;

    public AddGroupUserCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _repository = unitOfWork.Repository<Group>();
        _userRepository = _unitOfWork.Repository<ApplicationUser>();
    }
    public async Task<Unit> Handle(AddGroupUserCommand request, CancellationToken cancellationToken)
    {
        var query = _repository
            .SingleResultQuery()
            .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll)
            .Include(i => i.Include(x => x.GroupUsers).ThenInclude(x => x.User))
            .AndFilter(x => x.Id == request.Id);

        var mission = await _repository
            .SingleOrDefaultAsync(query, cancellationToken);

        if (mission == null) throw new NotFoundException(nameof(Group), request.Id);

        if (mission.GroupUsers.Any(x => x.User.UserName == request.UserName)) throw new InvalidOperationException($"{request.UserName} already exists");

        var userQuery = _userRepository
            .SingleResultQuery()
            .AndFilter(x => x.UserName == request.UserName);

        var user = await _userRepository
            .SingleOrDefaultAsync(userQuery, cancellationToken);

        if (user == null) throw new NotFoundException(nameof(ApplicationUser), request.UserName);

        mission.GroupUsers.Add(new GroupUser { User = user });

        await _unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

        _repository.RemoveTracking(mission);

        return Unit.Value;
    }
}
