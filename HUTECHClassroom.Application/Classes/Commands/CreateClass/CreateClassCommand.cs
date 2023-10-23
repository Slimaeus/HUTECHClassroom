using HUTECHClassroom.Application.Common.Requests;

namespace HUTECHClassroom.Application.Classes.Commands.CreateClass;

public sealed record CreateClassCommand(string Id) : CreateCommand<string>;
public sealed class CreateClassCommandHandler : CreateCommandHandler<string, Class, CreateClassCommand>
{
    public CreateClassCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}
