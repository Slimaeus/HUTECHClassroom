using HUTECHClassroom.Application.Scores.DTOs;
using HUTECHClassroom.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace HUTECHClassroom.Application.Classrooms.Commands.ImportScoreByClassroomId;

public sealed record ImportScoreByClassroomIdCommand(Guid ClassroomId, IFormFile File) : IRequest<Unit>;
public sealed class Hanlder : IRequestHandler<ImportScoreByClassroomIdCommand, Unit>
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
    public async Task<Unit> Handle(ImportScoreByClassroomIdCommand request, CancellationToken cancellationToken)
    {
        var importedStudentResults = _excelServie.ReadExcelFileWithColumnNames<StudentResultWithOrdinalDTO>(request.File.OpenReadStream(), null);

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
            studentResult.Score = sr.Score ?? studentResult.Score;
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken);

        return Unit.Value;
    }
}
