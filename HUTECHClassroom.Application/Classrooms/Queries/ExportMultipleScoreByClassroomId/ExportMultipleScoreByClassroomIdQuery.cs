using HUTECHClassroom.Domain.Interfaces;
using HUTECHClassroom.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace HUTECHClassroom.Application.Classrooms.Queries.ExportMultipleScoreByClassroomId;

public sealed record ExportMultipleScoreByClassroomIdQuery(Guid ClassroomId) : IRequest<byte[]>;
public sealed class Handler : IRequestHandler<ExportMultipleScoreByClassroomIdQuery, byte[]>
{
    private readonly IExcelService _excelServie;
    private readonly ApplicationDbContext _applicationDbContext;

    public Handler(IExcelService excelServie, ApplicationDbContext applicationDbContext)
    {
        _excelServie = excelServie;
        _applicationDbContext = applicationDbContext;
    }
    public sealed record ExportedClassroomMultipleScore(Guid? StudentId, string? StudentUserName, string LastName, string FirstName, Guid? ClassroomId, string ClassroomClass, double? Score1, double? Score2);
    public async Task<byte[]> Handle(ExportMultipleScoreByClassroomIdQuery request, CancellationToken cancellationToken)
    {
        var studentResults = await _applicationDbContext.StudentResults
            .Where(x => x.ClassroomId == request.ClassroomId)
            .Where(x => x.Student != null)
            .Where(x => x.Classroom != null)
            .GroupBy(x => new { x.StudentId, x.ClassroomId })
            .Select(g => new ExportedClassroomMultipleScore(
                g.Key.StudentId,
                g.First().Student!.UserName,
                g.First().Student!.LastName,
                g.First().Student!.FirstName,
                g.Key.ClassroomId,
                g.First().Classroom!.Class!.Name,
                g.Where(x => x.ScoreTypeId == 1).Select(x => x.Score).FirstOrDefault(),
                g.Where(x => x.ScoreTypeId == 2).Select(x => x.Score).FirstOrDefault()))
            .ToListAsync(cancellationToken: cancellationToken);

        Type type = typeof(ExportedClassroomMultipleScore);
        PropertyInfo[] propertyInfos = type.GetProperties();

        //var propertyNames = propertyInfos
        //    .Where(x => x.Name != "Id"
        //    && x.Name != "CreateDate"
        //    && x.Name != "UpdateDate"
        //    && x.CanRead
        //    && (x.PropertyType.IsEnum
        //        || x.PropertyType.Equals(typeof(DateTime))
        //        //|| x.PropertyType.Equals(typeof(Guid))
        //        || x.PropertyType.Equals(typeof(string))
        //    ))
        //    .Select(x => x.Name);
        var propertyNames = propertyInfos.Select(x => x.Name);

        return _excelServie.ExportToExcel(studentResults, propertyNames);
    }
}

