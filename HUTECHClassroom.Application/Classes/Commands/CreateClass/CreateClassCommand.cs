using HUTECHClassroom.Application.Common.Requests;

namespace HUTECHClassroom.Application.Classes.Commands.CreateClass;

public sealed record CreateClassCommand(string Name) : CreateCommand;
public sealed class CreateClassCommandHandler : CreateCommandHandler<Class, CreateClassCommand>
{
    public CreateClassCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}
