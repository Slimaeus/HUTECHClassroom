using HUTECHClassroom.Domain.Interfaces;
using HUTECHClassroom.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace HUTECHClassroom.Application.Classrooms.Queries.ExportScoreByClassroomId;

public sealed record ExportScoreByClassroomIdQuery(Guid ClassroomId, int ScoreTypeId) : IRequest<byte[]>;
public sealed class Handler : IRequestHandler<ExportScoreByClassroomIdQuery, byte[]>
{
    private readonly IExcelServie _excelServie;
    private readonly ApplicationDbContext _applicationDbContext;

    public Handler(IExcelServie excelServie, ApplicationDbContext applicationDbContext)
    {
        _excelServie = excelServie;
        _applicationDbContext = applicationDbContext;
    }
    public sealed record ExportedClassroomScore(Guid? StudentId, string? StudentUserName, string LastName, string FirstName, int? ScoreTypeId, Guid? ClassroomId, string ClassroomClass, double Score);
    public async Task<byte[]> Handle(ExportScoreByClassroomIdQuery request, CancellationToken cancellationToken)
    {
        var studentResults = await _applicationDbContext.StudentResults
            .Where(x => x.ClassroomId == request.ClassroomId)
            .Where(x => x.ScoreTypeId == request.ScoreTypeId)
            .Where(x => x.Student != null)
            .Where(x => x.Classroom != null)
            .Select(x => new ExportedClassroomScore(x.StudentId,
                x.Student!.UserName,
                x.Student!.LastName,
                x.Student!.FirstName,
                x.ScoreTypeId,
                x.ClassroomId,
                x.Classroom!.Class!.Name,
                x.Score))
            .ToListAsync(cancellationToken: cancellationToken);

        Type type = typeof(ExportedClassroomScore);
        PropertyInfo[] propertyInfos = type.GetProperties();

        var propertyNames = propertyInfos
            .Where(x => x.Name != "Id"
            && x.Name != "CreateDate"
            && x.Name != "UpdateDate"
            && x.CanRead
            && (x.PropertyType.IsPrimitive
                || x.PropertyType.IsEnum
                || x.PropertyType.Equals(typeof(DateTime))
                //|| x.PropertyType.Equals(typeof(Guid))
                || x.PropertyType.Equals(typeof(string))
            ))
            .Select(x => x.Name);

        return _excelServie.ExportToExcel(studentResults, propertyNames);
    }
}
