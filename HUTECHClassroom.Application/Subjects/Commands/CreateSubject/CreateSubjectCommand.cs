using HUTECHClassroom.Application.Common.Exceptions;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Subjects.DTOs;

namespace HUTECHClassroom.Application.Subjects.Commands.CreateSubject;

public record CreateSubjectCommand : CreateCommand<SubjectDTO>
{
    public string Id { get; set; }
    public string Title { get; set; }
    public int TotalCredits { get; set; }
    public string MajorId { get; set; }
}
public class CreateSubjectCommandHandler : CreateCommandHandler<string, Subject, CreateSubjectCommand, SubjectDTO>
{
    private readonly IRepository<Major> _majorRepository;

    public CreateSubjectCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
        _majorRepository = unitOfWork.Repository<Major>();
    }
    protected override async Task ValidateAdditionalField(CreateSubjectCommand request, Subject entity)
    {
        var majorQuery = _majorRepository
            .SingleResultQuery()
            .AndFilter(x => x.Id == request.MajorId);

        var major = await _majorRepository.SingleOrDefaultAsync(majorQuery);

        if (major == null) throw new NotFoundException(nameof(Major), request.MajorId);

        entity.Major = major;
    }
}
