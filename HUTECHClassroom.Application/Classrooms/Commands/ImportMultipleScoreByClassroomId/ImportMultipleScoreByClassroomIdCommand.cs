using HUTECHClassroom.Application.Common.Validators.Classrooms;
using HUTECHClassroom.Application.Scores.DTOs;
using HUTECHClassroom.Domain.Constants;
using HUTECHClassroom.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace HUTECHClassroom.Application.Classrooms.Commands.ImportMultipleScoreByClassroomId;

public sealed record ImportMultipleScoreByClassroomIdCommand(Guid ClassroomId, IFormFile File) : IRequest<Unit>;
public sealed class Hanlder : IRequestHandler<ImportMultipleScoreByClassroomIdCommand, Unit>
{
    private readonly IExcelService _excelService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<StudentResult> _studentResultRepository;
    private readonly IRepository<Classroom> _classroomRepository;
    private readonly IRepository<ApplicationUser> _userRepository;

    public Hanlder(IExcelService excelService, IUnitOfWork unitOfWork)
    {
        _excelService = excelService;
        _unitOfWork = unitOfWork;
        _studentResultRepository = unitOfWork.Repository<StudentResult>();
        _classroomRepository = unitOfWork.Repository<Classroom>();
        _userRepository = unitOfWork.Repository<ApplicationUser>();
    }
    public async Task<Unit> Handle(ImportMultipleScoreByClassroomIdCommand request, CancellationToken cancellationToken)
    {
        var importedStudentResults = _excelService.ReadExcelFileWithColumnNames<StudentResultScoresWithOrdinalDTO>(request.File.OpenReadStream(), null);

        var studentResultDictionary = importedStudentResults.ToDictionary(x => x.Id ?? "", x => x);

        var getStudentResultsQuery = _studentResultRepository
            .MultipleResultQuery()
            .Include(i => i.Include(x => x.Student))
            .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll)
            .AndFilter(x => x.ClassroomId == request.ClassroomId)
            .AndFilter(x => importedStudentResults.Select(x => x.Id).Contains(x.Student!.UserName));

        var studentResults = await _studentResultRepository
           .SearchAsync(getStudentResultsQuery, cancellationToken);

        var query = _userRepository
                .MultipleResultQuery()
                .AndFilter(x => studentResultDictionary.Keys.Contains(x.UserName))
                .AndFilter(x => x.ClassroomUsers.Any(u => u.ClassroomId == request.ClassroomId))
                .AndFilter(x => !x.StudentResults.Any(x => x.ClassroomId == request.ClassroomId));

        var usersInClassroom = await _userRepository
            .SearchAsync(query, cancellationToken);

        foreach (var user in usersInClassroom)
        {
            if (user.UserName is null) continue;
            var isValidResult = studentResultDictionary.TryGetValue(user.UserName, out var result);
            if (!isValidResult || result is null || (result.Score1 is null && result.Score2 is null)) continue;
            if (result.Score1 is { })
            {
                await _studentResultRepository.AddAsync(new StudentResult
                {
                    ClassroomId = request.ClassroomId,
                    ScoreTypeId = ScoreTypeConstants.MidtermScoreId,
                    StudentId = user.Id,
                    Score = result.Score1.Value
                }, cancellationToken);
            }
            if (result.Score2 is { })
            {
                await _studentResultRepository.AddAsync(new StudentResult
                {
                    ClassroomId = request.ClassroomId,
                    ScoreTypeId = ScoreTypeConstants.FinalScoreId,
                    StudentId = user.Id,
                    Score = result.Score2.Value
                }, cancellationToken);
            }
        }

        foreach (var studentResult in studentResults)
        {
            if (studentResult.Student is null || studentResult.Student.UserName is null) continue;
            var isValid = studentResultDictionary.TryGetValue(studentResult.Student.UserName, out var sr);
            if (!isValid || sr is null) continue;
            if (studentResult.ScoreTypeId == ScoreTypeConstants.MidtermScoreId)
            {
                studentResult.Score = sr.Score1 ?? studentResult.Score;
            }
            if (studentResult.ScoreTypeId == ScoreTypeConstants.FinalScoreId)
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