using EntityFrameworkCore.QueryBuilder.Interfaces;
using HUTECHClassroom.Application.Common.Extensions;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Faculties.DTOs;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Faculties.Queries.GetFacultiesWithPagination;

public record GetFacultiesWithPaginationQuery(FacultyPaginationParams Params) : GetWithPaginationQuery<FacultyDTO, FacultyPaginationParams>(Params);
public class GetFacultiesWithPaginationQueryHandler : GetWithPaginationQueryHandler<Faculty, GetFacultiesWithPaginationQuery, FacultyDTO, FacultyPaginationParams>
{
    public GetFacultiesWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }
    protected override Expression<Func<Faculty, bool>> SearchStringPredicate(string searchString)
        => x => x.Name.ToLower().Contains(searchString.ToLower());

    protected override Expression<Func<Faculty, object>> OrderByKeySelector()
        => x => x.CreateDate;
    protected override IMultipleResultQuery<Faculty> SortingQuery(IMultipleResultQuery<Faculty> query, GetFacultiesWithPaginationQuery request)
        => query.SortEntityQuery(request.Params.NameOrder, x => x.Name);
}

