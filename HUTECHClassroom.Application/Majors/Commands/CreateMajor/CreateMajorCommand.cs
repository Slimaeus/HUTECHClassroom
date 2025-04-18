using HUTECHClassroom.Application.Common.Requests;

namespace HUTECHClassroom.Application.Majors.Commands.CreateMajor;

public record CreateMajorCommand : CreateCommand
{
    public required string Code { get; set; }
    public string? Title { get; set; }
    public int TotalCredits { get; set; }
    public int NonComulativeCredits { get; set; }
    public Guid? FacultyId { get; set; }
}
public sealed class CreateMajorCommandHandler : CreateCommandHandler<Major, CreateMajorCommand>
{
    public CreateMajorCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}
