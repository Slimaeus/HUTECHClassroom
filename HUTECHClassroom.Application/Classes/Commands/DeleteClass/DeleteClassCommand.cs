using HUTECHClassroom.Application.Classes.DTOs;
using HUTECHClassroom.Application.Common.Requests;

namespace HUTECHClassroom.Application.Classs.Commands.DeleteClass;

public record DeleteClassCommand(string Id) : DeleteCommand<string, ClassDTO>(Id);
public class DeleteClassCommandHandler : DeleteCommandHandler<string, Class, DeleteClassCommand, ClassDTO>
{
    public DeleteClassCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}
