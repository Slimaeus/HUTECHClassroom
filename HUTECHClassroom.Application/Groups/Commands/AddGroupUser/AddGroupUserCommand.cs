using EntityFrameworkCore.Repository.Extensions;
using HUTECHClassroom.Domain.Constants;
using Microsoft.EntityFrameworkCore;

namespace HUTECHClassroom.Application.Groups.Commands.AddGroupUser;

public record AddGroupUserCommand(Guid GroupId, Guid UserId) : IRequest<Unit>;
public sealed class AddGroupUserCommandHandler : IRequestHandler<AddGroupUserCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IRepository<Group> _repository;
    private readonly IRepository<GroupRole> _groupRoleRepository;

    public AddGroupUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _repository = unitOfWork.Repository<Group>();
        _groupRoleRepository = _unitOfWork.Repository<GroupRole>();
    }
    public async Task<Unit> Handle(AddGroupUserCommand request, CancellationToken cancellationToken)
    {
        var query = _repository
            .SingleResultQuery()
            .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll)
            .Include(i => i.Include(x => x.GroupUsers).ThenInclude(x => x.User))
            .AndFilter(x => x.Id == request.GroupId);

        var group = await _repository
            .SingleOrDefaultAsync(query, cancellationToken);

        if (group.GroupUsers.Any(x => x.UserId == request.UserId)) throw new InvalidOperationException($"{request.UserId} already exists");

        var groupUser = _mapper.Map<GroupUser>(request);

        var groupRoleQuery = _groupRoleRepository
            .SingleResultQuery()
            .AndFilter(x => x.Name == GroupRoleConstants.MEMBER);

        var groupRole = await _groupRoleRepository.SingleOrDefaultAsync(groupRoleQuery, cancellationToken);

        groupUser.GroupRole = groupRole;

        group.GroupUsers.Add(groupUser);

        await _unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

        _repository.RemoveTracking(group);

        return Unit.Value;
    }
}
