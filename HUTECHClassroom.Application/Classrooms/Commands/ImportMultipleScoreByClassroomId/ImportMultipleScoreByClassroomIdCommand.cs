using HUTECHClassroom.Application.Common.Validators.Classrooms;
using HUTECHClassroom.Application.Scores.DTOs;
using HUTECHClassroom.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace HUTECHClassroom.Application.Classrooms.Commands.ImportMultipleScoreByClassroomId;

public sealed record ImportMultipleScoreByClassroomIdCommand(Guid ClassroomId, IFormFile File) : IRequest<Unit>;
public sealed class Hanlder : IRequestHandler<ImportMultipleScoreByClassroomIdCommand, Unit>
{
    private readonly IExcelServie _excelServie;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<StudentResult> _studentResultRepository;

    public Hanlder(IExcelServie excelServie, IUnitOfWork unitOfWork)
    {
        _excelServie = excelServie;
        _unitOfWork = unitOfWork;
        _studentResultRepository = unitOfWork.Repository<StudentResult>();
    }
    public async Task<Unit> Handle(ImportMultipleScoreByClassroomIdCommand request, CancellationToken cancellationToken)
    {
        var importedStudentResults = _excelServie.ReadExcelFileWithColumnNames<StudentResultScoresWithOrdinalDTO>(request.File.OpenReadStream(), null);

        var studentResultDictionary = importedStudentResults.ToDictionary(x => x.Id ?? "", x => x);

        var getStudentResultsQuery = _studentResultRepository
            .MultipleResultQuery()
            .Include(i => i.Include(x => x.Student))
            .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll)
            .AndFilter(x => x.ClassroomId == request.ClassroomId)
            .AndFilter(x => importedStudentResults.Select(x => x.Id).Contains(x.Student!.UserName));

        var studentResults = await _studentResultRepository
           .SearchAsync(getStudentResultsQuery, cancellationToken);

        foreach (var studentResult in studentResults)
        {
            if (studentResult.Student is null || studentResult.Student.UserName is null) continue;
            var isValid = studentResultDictionary.TryGetValue(studentResult.Student.UserName, out var sr);
            if (!isValid || sr is null) continue;
            if (sr.Score1 is { } && studentResult.ScoreTypeId == 1)
            {
                studentResult.Score = sr.Score1 ?? studentResult.Score;
            }
            if (sr.Score2 is { } && studentResult.ScoreTypeId == 2)
            {
                studentResult.Score = sr.Score2 ?? studentResult.Score;
            }
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken);

        return Unit.Value;
    }
}

public sealed class Validator : AbstractValidator<ImportMultipleScoreByClassroomIdCommand>
{
    public Validator(ClassroomExistenceByNotNullIdValidator classroomIdValidator)
    {
        RuleFor(x => x.ClassroomId)
            .SetValidator(classroomIdValidator);
    }
}