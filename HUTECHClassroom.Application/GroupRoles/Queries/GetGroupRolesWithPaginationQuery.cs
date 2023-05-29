using EntityFrameworkCore.QueryBuilder.Interfaces;
using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.GroupRoles.DTOs;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.GroupRoles.Queries;

public record GetGroupRolesWithPaginationQuery(PaginationParams Params) : GetWithPaginationQuery<GroupRoleDTO, PaginationParams>(Params);
public class GetGroupRolesWithPaginationQueryHandler : GetWithPaginationQueryHandler<GroupRole, GetGroupRolesWithPaginationQuery, GroupRoleDTO, PaginationParams>
{
    public GetGroupRolesWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
    protected override Expression<Func<GroupRole, bool>> SearchStringPredicate(string searchString)
    {
        return x => x.Name.ToLower().Equals(searchString.ToLower());
    }
    protected override IQuery<GroupRole> Order(IMultipleResultQuery<GroupRole> query) => query.OrderByDescending(x => x.Name);
}
