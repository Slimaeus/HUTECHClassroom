using EntityFrameworkCore.QueryBuilder.Interfaces;
using HUTECHClassroom.Application.Common.Extensions;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Roles.DTOs;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Roles.Queries.GetRolesWithPagination;

public sealed record GetRolesWithPaginationQuery(RolePaginationParams Params) : GetWithPaginationQuery<RoleDTO, RolePaginationParams>(Params);
public sealed class GetRolesWithPaginationQueryHandler : GetWithPaginationQueryHandler<ApplicationRole, GetRolesWithPaginationQuery, RoleDTO, RolePaginationParams>
{
    public GetRolesWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }
    protected override Expression<Func<ApplicationRole, bool>> SearchStringPredicate(string searchString)
        => x => x.Name != null && x.Name.ToLower().Contains(searchString.ToLower());
    protected override IQuery<ApplicationRole> Order(IMultipleResultQuery<ApplicationRole> query)
        => query.OrderBy(x => x.Name);
    protected override IMultipleResultQuery<ApplicationRole> SortingQuery(IMultipleResultQuery<ApplicationRole> query, GetRolesWithPaginationQuery request)
        => query.SortEntityQuery(request.Params.NameOrder, x => x.Name);
}
