using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Faculties.DTOs;

namespace HUTECHClassroom.Application.Faculties.Commands.DeleteFaculty;

public record DeleteFacultyCommand(Guid Id) : DeleteCommand<FacultyDTO>(Id);
public class DeleteFacultyCommandHandler : DeleteCommandHandler<Faculty, DeleteFacultyCommand, FacultyDTO>
{
    public DeleteFacultyCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}
