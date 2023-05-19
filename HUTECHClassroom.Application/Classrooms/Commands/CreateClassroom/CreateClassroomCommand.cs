using HUTECHClassroom.Application.Classrooms.DTOs;
using HUTECHClassroom.Application.Common.Exceptions;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Domain.Enums;

namespace HUTECHClassroom.Application.Classrooms.Commands.CreateClassroom;

public record CreateClassroomCommand : CreateCommand<ClassroomDTO>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Topic { get; set; }
    public string Room { get; set; }
    public string Class { get; set; }
    public string SchoolYear { get; set; }
    public string StudyGroup { get; set; }
    public string PracticalStudyGroup { get; set; }
    public Semester Semester { get; set; } = Semester.I;
    public ClassroomType Type { get; set; } = ClassroomType.TheoryRoom;
    public Guid FacultyId { get; set; }
    public string LecturerName { get; set; }
    public Guid SubjectId { get; set; }
}
public class CreateClassroomCommandHandler : CreateCommandHandler<Classroom, CreateClassroomCommand, ClassroomDTO>
{
    private readonly IRepository<ApplicationUser> _userRepository;
    private readonly IRepository<Faculty> _facultyRepository;
    private readonly IRepository<Subject> _subjectRepository;

    public CreateClassroomCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
        _userRepository = unitOfWork.Repository<ApplicationUser>();
        _facultyRepository = unitOfWork.Repository<Faculty>();
        _subjectRepository = unitOfWork.Repository<Subject>();
    }
    protected override async Task ValidateAdditionalField(CreateClassroomCommand request, Classroom entity)
    {
        var userQuery = _userRepository
            .SingleResultQuery()
            .AndFilter(x => x.UserName == request.LecturerName);

        var lecturer = await _userRepository.SingleOrDefaultAsync(userQuery);

        if (lecturer == null) throw new NotFoundException(nameof(ApplicationUser), request.LecturerName);

        entity.Lecturer = lecturer;

        if (request.FacultyId == Guid.Empty) return;

        var facultyQuery = _facultyRepository
            .SingleResultQuery()
            .AndFilter(x => x.Id == request.FacultyId);

        var faculty = await _facultyRepository.SingleOrDefaultAsync(facultyQuery);

        if (faculty == null) throw new NotFoundException(nameof(Faculty), request.FacultyId);

        entity.Faculty = faculty;

        if (request.SubjectId == Guid.Empty) return;

        var subjectQuery = _subjectRepository
            .SingleResultQuery()
            .AndFilter(x => x.Id == request.SubjectId);

        var subject = await _subjectRepository.SingleOrDefaultAsync(subjectQuery);

        if (subject == null) throw new NotFoundException(nameof(Faculty), request.FacultyId);

        entity.Subject = subject;
    }
}
