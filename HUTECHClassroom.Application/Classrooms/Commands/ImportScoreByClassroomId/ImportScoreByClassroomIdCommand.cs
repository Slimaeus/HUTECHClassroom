using HUTECHClassroom.Application.Common.Validators.Classrooms;
using HUTECHClassroom.Application.Common.Validators.ScoreTypes;
using HUTECHClassroom.Application.Scores.DTOs;
using HUTECHClassroom.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace HUTECHClassroom.Application.Classrooms.Commands.ImportScoreByClassroomId;

public sealed record ImportScoreByClassroomIdCommand(Guid ClassroomId, int ScoreTypeId, IFormFile File) : IRequest<Unit>;
public sealed class Hanlder : IRequestHandler<ImportScoreByClassroomIdCommand, Unit>
{
    private readonly IExcelServie _excelServie;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<StudentResult> _studentResultRepository;
    private readonly IRepository<Classroom> _classroomRepository;
    private readonly IRepository<ApplicationUser> _userRepository;

    public Hanlder(IExcelServie excelServie, IUnitOfWork unitOfWork)
    {
        _excelServie = excelServie;
        _unitOfWork = unitOfWork;
        _studentResultRepository = unitOfWork.Repository<StudentResult>();
        _classroomRepository = unitOfWork.Repository<Classroom>();
        _userRepository = unitOfWork.Repository<ApplicationUser>();
    }
    public async Task<Unit> Handle(ImportScoreByClassroomIdCommand request, CancellationToken cancellationToken)
    {
        var importedStudentResults = _excelServie.ReadExcelFileWithColumnNames<StudentResultWithOrdinalDTO>(request.File.OpenReadStream(), null);

        var studentResultDictionary = importedStudentResults.ToDictionary(x => x.Id ?? "", x => x);

        var getStudentResultsQuery = _studentResultRepository
            .MultipleResultQuery()
            .Include(i => i.Include(x => x.Student))
            .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll)
            .AndFilter(x => x.ClassroomId == request.ClassroomId)
            .AndFilter(x => x.ScoreTypeId == request.ScoreTypeId)
            .AndFilter(x => importedStudentResults.Select(x => x.Id).Contains(x.Student!.UserName));

        var studentResults = await _studentResultRepository
           .SearchAsync(getStudentResultsQuery, cancellationToken);

        var query = _userRepository
                .MultipleResultQuery()
                .AndFilter(x => studentResultDictionary.Keys.Contains(x.UserName))
                .AndFilter(x => x.ClassroomUsers.Any(u => u.ClassroomId == request.ClassroomId))
                .AndFilter(x => !x.StudentResults.Any(x => x.ClassroomId == request.ClassroomId && x.ScoreTypeId == request.ScoreTypeId));

        var usersInClassroom = await _userRepository
            .SearchAsync(query, cancellationToken);

        foreach (var user in usersInClassroom)
        {
            if (user.UserName is null) continue;
            var isValidResult = studentResultDictionary.TryGetValue(user.UserName, out var result);
            if (!isValidResult || result is null || result.Score is null) continue;
            await _studentResultRepository.AddAsync(new StudentResult
            {
                ClassroomId = request.ClassroomId,
                ScoreTypeId = request.ScoreTypeId,
                StudentId = user.Id,
                Score = result.Score.Value
            }, cancellationToken);
        }

        foreach (var studentResult in studentResults)
        {
            if (studentResult.Student is null || studentResult.Student.UserName is null) continue;
            var isValid = studentResultDictionary.TryGetValue(studentResult.Student.UserName, out var sr);
            if (!isValid || sr is null) continue;
            studentResult.Score = sr.Score ?? studentResult.Score;
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken);

        return Unit.Value;
    }
}

public sealed class Validator : AbstractValidator<ImportScoreByClassroomIdCommand>
{
    public Validator(ClassroomExistenceByNotNullIdValidator classroomIdValidator, ScoreTypeExistenceByNotNullIdValidator scoreTypeIdValidor)
    {
        RuleFor(x => x.ClassroomId)
            .SetValidator(classroomIdValidator);

        RuleFor(x => x.ScoreTypeId)
            .SetValidator(scoreTypeIdValidor);
    }
}