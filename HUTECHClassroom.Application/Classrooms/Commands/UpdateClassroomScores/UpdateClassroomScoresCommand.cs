using HUTECHClassroom.Application.Common.Validators.Classrooms;
using HUTECHClassroom.Application.Common.Validators.ScoreTypes;
using Microsoft.EntityFrameworkCore;

namespace HUTECHClassroom.Application.Classrooms.Commands.UpdateClassroomScores;

public sealed record UpdateClassroomScoresCommand(Guid ClassroomId, int ScoreTypeId, double Score) : IRequest<Unit>;
public sealed class Handler : IRequestHandler<UpdateClassroomScoresCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<StudentResult> _repository;

    public Handler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _repository = unitOfWork.Repository<StudentResult>();
    }
    public async Task<Unit> Handle(UpdateClassroomScoresCommand request, CancellationToken cancellationToken)
    {
        var query = _repository
            .SingleResultQuery()
            .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll)
            .AndFilter(m => m.ClassroomId.Equals(request.ClassroomId))
            .AndFilter(m => m.ScoreTypeId.Equals(request.ScoreTypeId));

        var entity = await _repository
            .FirstOrDefaultAsync(query, cancellationToken);

        if (entity is null)
            await _repository.AddAsync(new StudentResult
            {
                ClassroomId = request.ClassroomId,
                ScoreTypeId = request.ScoreTypeId,
                Score = request.Score
            }, cancellationToken);
        else
            entity.Score = request.Score;

        await _unitOfWork
            .SaveChangesAsync(cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        return Unit.Value;
    }
}

public sealed class Validator : AbstractValidator<UpdateClassroomScoresCommand>
{
    public Validator(ClassroomExistenceByNotNullIdValidator classroomIdValidator, ScoreTypeExistenceByNotNullIdValidator scoreTypeIdValidator)
    {
        RuleFor(x => x.ClassroomId)
            .SetValidator(classroomIdValidator);

        RuleFor(x => x.ScoreTypeId)
            .SetValidator(scoreTypeIdValidator);
    }
}
