using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Faculties.DTOs;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Faculties.Queries.GetFaculty;

public record GetFacultyQuery(Guid Id) : GetQuery<FacultyDTO>;
public class GetFacultyQueryHandler : GetQueryHandler<Faculty, GetFacultyQuery, FacultyDTO>
{
    public GetFacultyQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }
    public override Expression<Func<Faculty, bool>> FilterPredicate(GetFacultyQuery query)
    {
        return x => x.Id == query.Id;
    }
    public override object GetNotFoundKey(GetFacultyQuery query)
    {
        return query.Id;
    }
}
