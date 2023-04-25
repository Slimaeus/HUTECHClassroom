using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Faculties.DTOs;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Faculties.Queries.GetFacultiesWithPagination;

public record GetFacultiesWithPaginationQuery(PaginationParams Params) : GetWithPaginationQuery<FacultyDTO>(Params);
public class GetFacultiesWithPaginationQueryHandler : GetWithPaginationQueryHandler<Faculty, GetFacultiesWithPaginationQuery, FacultyDTO>
{
    public GetFacultiesWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }
    protected override Expression<Func<Faculty, bool>> SearchStringPredicate(string searchString) =>
        x => x.Name.ToLower().Contains(searchString.ToLower());
    protected override Expression<Func<Faculty, object>> OrderByKeySelector()
    {
        return x => x.CreateDate;
    }
}

