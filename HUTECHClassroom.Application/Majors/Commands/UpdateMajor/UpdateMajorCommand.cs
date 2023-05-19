using HUTECHClassroom.Application.Common.Requests;

namespace HUTECHClassroom.Application.Majors.Commands.UpdateMajor;

public record UpdateMajorCommand(Guid Id) : UpdateCommand(Id)
{
    public string Code { get; set; }
    public string Title { get; set; }
    public int TotalCredits { get; set; }
    public int NonComulativeCredits { get; set; }
}
public class UpdateMajorCommandHandler : UpdateCommandHandler<Major, UpdateMajorCommand>
{
    public UpdateMajorCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}
