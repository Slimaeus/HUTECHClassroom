using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Domain.Constants;

namespace HUTECHClassroom.Application.Groups.Commands.CreateGroup;

public record CreateGroupCommand : CreateCommand
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid LeaderId { get; set; }
    public Guid ClassroomId { get; set; }
}
public class CreateGroupCommandHandler : CreateCommandHandler<Group, CreateGroupCommand>
{
    private readonly IRepository<GroupRole> _groupRoleRepository;

    public CreateGroupCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
        _groupRoleRepository = unitOfWork.Repository<GroupRole>();
    }

    protected override async Task ValidateAdditionalField(CreateGroupCommand request, Group entity)
    {
        var groupRoleQuery = _groupRoleRepository.SingleResultQuery().AndFilter(x => x.Name == GroupRoleConstants.LEADER);
        entity.GroupUsers.Add(new GroupUser { UserId = request.LeaderId, GroupRole = await _groupRoleRepository.SingleOrDefaultAsync(groupRoleQuery) });
    }
}
