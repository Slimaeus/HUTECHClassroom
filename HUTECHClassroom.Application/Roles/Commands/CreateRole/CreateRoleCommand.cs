using HUTECHClassroom.Application.Common.Requests;

namespace HUTECHClassroom.Application.Roles.Commands.CreateRole;

public sealed record CreateRoleCommand : CreateCommand
{
    public required string Name { get; set; }
}
public sealed class CreateRoleCommandHandler : CreateCommandHandler<ApplicationRole, CreateRoleCommand>
{
    public CreateRoleCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}
