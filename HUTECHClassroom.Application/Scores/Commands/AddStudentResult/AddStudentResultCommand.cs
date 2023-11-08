using FluentValidation.Results;
using HUTECHClassroom.Application.Common.Validators.Classrooms;
using HUTECHClassroom.Application.Common.Validators.ScoreTypes;
using HUTECHClassroom.Application.Common.Validators.Users;
using HUTECHClassroom.Persistence;

namespace HUTECHClassroom.Application.Scores.Commands.AddStudentResult;

public sealed record AddStudentResultCommand(Guid StudentId, Guid ClassroomId, int ScoreTypeId, int OrdinalNumber, int Score) : IRequest<Unit>;
public sealed class Handler : IRequestHandler<AddStudentResultCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly IMapper _mapper;

    public Handler(IUnitOfWork unitOfWork, ApplicationDbContext applicationDbContext, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _applicationDbContext = applicationDbContext;
        _mapper = mapper;
    }
    public async Task<Unit> Handle(AddStudentResultCommand request, CancellationToken cancellationToken)
    {
        if (_applicationDbContext.StudentResults.Any(x => x.StudentId == request.StudentId && x.ClassroomId == request.ClassroomId && x.ScoreTypeId == request.ScoreTypeId))
        {
            var failures = new List<ValidationFailure>
            {
                new ValidationFailure("Result", "Result already existed")
            };
            throw new ValidationException(failures);
        }
        var studentResult = _mapper.Map<StudentResult>(request);

        await _unitOfWork.DbContext.AddAsync(studentResult, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken);

        return Unit.Value;
    }
}
public sealed class Validator : AbstractValidator<AddStudentResultCommand>
{
    public Validator(
        UserExistenceByNotNullIdValidator userValidator,
        ClassroomExistenceByNotNullIdValidator classroomValidator,
        ScoreTypeExistenceByNotNullIdValidator scoreTypeValidator)
    {
        RuleFor(x => x.StudentId)
            .SetValidator(userValidator);

        RuleFor(x => x.ClassroomId)
            .SetValidator(classroomValidator);

        RuleFor(x => x.ScoreTypeId)
            .SetValidator(scoreTypeValidator);
    }
}
