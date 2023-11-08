using HUTECHClassroom.Application.Common.Requests;

namespace HUTECHClassroom.Application.Classs.Commands.UpdateClass;

public sealed record UpdateClassCommand(Guid Id, string? Name) : UpdateCommand(Id);
public sealed class UpdateClassCommandHandler : UpdateCommandHandler<Class, UpdateClassCommand>
{
    public UpdateClassCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}
