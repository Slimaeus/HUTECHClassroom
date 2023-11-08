using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Majors.DTOs;

namespace HUTECHClassroom.Application.Majors.Commands.DeleteMajor;

public record DeleteMajorCommand(Guid Id) : DeleteCommand<MajorDTO>(Id);
public sealed class DeleteMajorCommandHandler : DeleteCommandHandler<Major, DeleteMajorCommand, MajorDTO>
{
    public DeleteMajorCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}
