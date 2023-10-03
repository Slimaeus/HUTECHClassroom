using HUTECHClassroom.Application.Common.Requests;

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

    public CreateGroupCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }
}
