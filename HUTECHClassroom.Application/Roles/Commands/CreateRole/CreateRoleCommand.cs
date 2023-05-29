using HUTECHClassroom.Application.Common.Requests;

namespace HUTECHClassroom.Application.Roles.Commands.CreateRole;

public record CreateRoleCommand : CreateCommand
{
    public string Name { get; set; }
}
public class CreateRoleCommandHandler : CreateCommandHandler<ApplicationRole, CreateRoleCommand>
{
    public CreateRoleCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}
