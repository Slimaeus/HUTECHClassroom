using HUTECHClassroom.Application.Common.Requests;

namespace HUTECHClassroom.Application.Classs.Commands.UpdateClass;

public record UpdateClassCommand(string Id) : UpdateCommand<string>(Id);
public class UpdateClassCommandHandler : UpdateCommandHandler<string, Class, UpdateClassCommand>
{
    public UpdateClassCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}
