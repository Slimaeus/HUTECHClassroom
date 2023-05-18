using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Majors.DTOs;

namespace HUTECHClassroom.Application.Majors.Commands.DeleteMajor;

public record DeleteMajorCommand(string Id) : DeleteCommand<string, MajorDTO>(Id);
public class DeleteMajorCommandHandler : DeleteCommandHandler<string, Major, DeleteMajorCommand, MajorDTO>
{
    public DeleteMajorCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}
