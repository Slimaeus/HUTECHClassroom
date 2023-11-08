using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Groups.DTOs;

namespace HUTECHClassroom.Application.Groups.Commands.DeleteGroup;

public record DeleteGroupCommand(Guid Id) : DeleteCommand<GroupDTO>(Id);
public sealed class DeleteGroupCommandHandler : DeleteCommandHandler<Group, DeleteGroupCommand, GroupDTO>
{
    public DeleteGroupCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}
