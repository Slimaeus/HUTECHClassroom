using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Majors.DTOs;

namespace HUTECHClassroom.Application.Majors.Commands.CreateMajor;

public record CreateMajorCommand : CreateCommand<MajorDTO>
{
    public string Code { get; set; }
    public string Title { get; set; }
    public int TotalCredits { get; set; }
    public int NonComulativeCredits { get; set; }
}
public class CreateMajorCommandHandler : CreateCommandHandler<Major, CreateMajorCommand, MajorDTO>
{
    public CreateMajorCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}
