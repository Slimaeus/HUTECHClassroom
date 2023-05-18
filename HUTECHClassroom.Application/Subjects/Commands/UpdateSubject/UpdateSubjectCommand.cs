using HUTECHClassroom.Application.Common.Requests;

namespace HUTECHClassroom.Application.Subjects.Commands.UpdateSubject;

public record UpdateSubjectCommand(string Id) : UpdateCommand<string>(Id)
{
    public string Title { get; set; } = string.Empty;
    public int TotalCredits { get; set; }
}
public class UpdateSubjectCommandHandler : UpdateCommandHandler<string, Subject, UpdateSubjectCommand>
{
    public UpdateSubjectCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}
