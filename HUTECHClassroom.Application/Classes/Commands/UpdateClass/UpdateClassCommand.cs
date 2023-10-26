using HUTECHClassroom.Application.Common.Requests;

namespace HUTECHClassroom.Application.Classs.Commands.UpdateClass;

public record UpdateClassCommand(Guid Id) : UpdateCommand(Id);
public class UpdateClassCommandHandler : UpdateCommandHandler<Class, UpdateClassCommand>
{
    public UpdateClassCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}
