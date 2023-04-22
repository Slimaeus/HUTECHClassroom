using HUTECHClassroom.Application.Answers.DTOs;
using HUTECHClassroom.Application.Common.Exceptions;
using HUTECHClassroom.Application.Common.Requests;

namespace HUTECHClassroom.Application.Answers.Commands.CreateAnswer;

public record CreateAnswerCommand : CreateCommand<AnswerDTO>
{
    public string Description { get; set; }
    public string Link { get; set; }
    public float Score { get; set; }
    public string UserName { get; set; }
    public Guid ExerciseId { get; set; }
}
public class CreateAnswerCommandHandler : CreateCommandHandler<Answer, CreateAnswerCommand, AnswerDTO>
{
    private readonly IRepository<ApplicationUser> _userRepository;
    private readonly IRepository<Exercise> _exerciseRepository;
    public CreateAnswerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
        _userRepository = unitOfWork.Repository<ApplicationUser>();
        _exerciseRepository = unitOfWork.Repository<Exercise>();
    }
    protected override async Task ValidateAdditionalField(CreateAnswerCommand request, Answer entity)
    {
        var userQuery = _userRepository
            .SingleResultQuery()
            .AndFilter(x => x.UserName == request.UserName);

        var user = await _userRepository.SingleOrDefaultAsync(userQuery);

        if (user == null) throw new NotFoundException(nameof(ApplicationUser), request.UserName);

        entity.User = user;

        var exerciseQuery = _exerciseRepository
            .SingleResultQuery()
            .AndFilter(x => x.Id == request.ExerciseId);

        var exercise = await _exerciseRepository.SingleOrDefaultAsync(exerciseQuery);

        if (exercise == null) throw new NotFoundException(nameof(Exercise), request.ExerciseId);

        entity.Exercise = exercise;
    }
}
