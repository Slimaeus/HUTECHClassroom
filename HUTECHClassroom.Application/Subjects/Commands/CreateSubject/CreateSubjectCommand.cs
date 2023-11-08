using HUTECHClassroom.Application.Common.Requests;

namespace HUTECHClassroom.Application.Subjects.Commands.CreateSubject;

public sealed record CreateSubjectCommand : CreateCommand
{
    public required string Code { get; set; }
    public string? Title { get; set; }
    public int TotalCredits { get; set; }
    public Guid MajorId { get; set; }
}
public sealed class CreateSubjectCommandHandler : CreateCommandHandler<Subject, CreateSubjectCommand>
{

    public CreateSubjectCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}
