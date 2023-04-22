using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Roles.DTOs;

namespace HUTECHClassroom.Application.Roles.Commands.CreateRole;

public record CreateRoleCommand : CreateCommand<RoleDTO>
{
    public string Name { get; set; }
}
public class CreateRoleCommandHandler : CreateCommandHandler<ApplicationRole, CreateRoleCommand, RoleDTO>
{
    public CreateRoleCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}
