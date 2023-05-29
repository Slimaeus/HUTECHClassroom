using HUTECHClassroom.Application.Common.Requests;

namespace HUTECHClassroom.Application.Faculties.Commands.CreateFaculty;

public record CreateFacultyCommand : CreateCommand
{
    public string Name { get; set; }
}
public class CreateFacultyCommandHandler : CreateCommandHandler<Faculty, CreateFacultyCommand>
{
    public CreateFacultyCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}
