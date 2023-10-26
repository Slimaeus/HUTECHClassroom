using HUTECHClassroom.Application.Classes.DTOs;
using HUTECHClassroom.Application.Common.Requests;

namespace HUTECHClassroom.Application.Classs.Commands.DeleteClass;

public record DeleteClassCommand(Guid Id) : DeleteCommand<ClassDTO>(Id);
public class DeleteClassCommandHandler : DeleteCommandHandler<Class, DeleteClassCommand, ClassDTO>
{
    public DeleteClassCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}
