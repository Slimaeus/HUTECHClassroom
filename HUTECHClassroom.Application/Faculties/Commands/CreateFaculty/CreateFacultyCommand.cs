using HUTECHClassroom.Application.Common.Requests;

namespace HUTECHClassroom.Application.Faculties.Commands.CreateFaculty;

public sealed record CreateFacultyCommand : CreateCommand
{
    public required string Name { get; set; }
}
public sealed class CreateFacultyCommandHandler : CreateCommandHandler<Faculty, CreateFacultyCommand>
{
    public CreateFacultyCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}
