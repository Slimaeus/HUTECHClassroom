using EntityFrameworkCore.QueryBuilder.Interfaces;
using HUTECHClassroom.Application.Common.Extensions;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Faculties.DTOs;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Faculties.Queries.GetFacultiesWithPagination;

public sealed record GetFacultiesWithPaginationQuery(FacultyPaginationParams Params) : GetWithPaginationQuery<FacultyDTO, FacultyPaginationParams>(Params);
public sealed class GetFacultiesWithPaginationQueryHandler : GetWithPaginationQueryHandler<Faculty, GetFacultiesWithPaginationQuery, FacultyDTO, FacultyPaginationParams>
{
    public GetFacultiesWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }
    protected override Expression<Func<Faculty, bool>> SearchStringPredicate(string searchString)
        => x => x.Name.ToLower().Contains(searchString.ToLower());

    protected override IQuery<Faculty> Order(IMultipleResultQuery<Faculty> query) => query.OrderByDescending(x => x.CreateDate);

    protected override IMultipleResultQuery<Faculty> SortingQuery(IMultipleResultQuery<Faculty> query, GetFacultiesWithPaginationQuery request)
        => query.SortEntityQuery(request.Params.NameOrder, x => x.Name);
}

