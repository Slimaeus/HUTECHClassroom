using HUTECHClassroom.Application.Common.Requests;

namespace HUTECHClassroom.Application.Groups.Commands.UpdateGroup;

public sealed record UpdateGroupCommand(Guid Id) : UpdateCommand(Id)
{
    public string? Name { get; set; }
    public string? Description { get; set; }
}
public sealed class UpdateGroupCommandHandler : UpdateCommandHandler<Group, UpdateGroupCommand>
{
    public UpdateGroupCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}
