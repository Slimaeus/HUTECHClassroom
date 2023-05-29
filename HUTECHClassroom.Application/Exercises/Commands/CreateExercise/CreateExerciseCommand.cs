using HUTECHClassroom.Application.Common.Exceptions;
using HUTECHClassroom.Application.Common.Requests;

namespace HUTECHClassroom.Application.Exercises.Commands.CreateExercise;

public record CreateExerciseCommand : CreateCommand
{
    public string Title { get; set; }
    public string Instruction { get; set; }
    public string Link { get; set; }
    public float TotalScore { get; set; }
    public DateTime Deadline { get; set; }
    public string Topic { get; set; }
    public string Criteria { get; set; }
    public Guid ClassroomId { get; set; }
}
public class CreateExerciseCommandHandler : CreateCommandHandler<Exercise, CreateExerciseCommand>
{
    private readonly IRepository<Classroom> _classroomRepository;

    public CreateExerciseCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
        _classroomRepository = unitOfWork.Repository<Classroom>();
    }
    protected override async Task ValidateAdditionalField(CreateExerciseCommand request, Exercise entity)
    {
        var classroomQuery = _classroomRepository
            .SingleResultQuery()
            .AndFilter(x => x.Id == request.ClassroomId);

        var classroom = await _classroomRepository.SingleOrDefaultAsync(classroomQuery);

        if (classroom == null) throw new NotFoundException(nameof(Classroom), request.ClassroomId);

        entity.Classroom = classroom;
    }
}
