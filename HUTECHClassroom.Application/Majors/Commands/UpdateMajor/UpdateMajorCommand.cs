using HUTECHClassroom.Application.Common.Requests;

namespace HUTECHClassroom.Application.Majors.Commands.UpdateMajor;

public record UpdateMajorCommand(string Id) : UpdateCommand<string>(Id)
{
    public string Title { get; set; }
    public int TotalCredits { get; set; }
    public int NonComulativeCredits { get; set; }
}
public class UpdateMajorCommandHandler : UpdateCommandHandler<string, Major, UpdateMajorCommand>
{
    public UpdateMajorCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}
