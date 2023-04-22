using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Roles.DTOs;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Roles.Queries.GetRolesWithPagination;

public record GetRolesWithPaginationQuery(PaginationParams Params) : GetWithPaginationQuery<RoleDTO>(Params);
public class GetRolesWithPaginationQueryHandler : GetWithPaginationQueryHandler<ApplicationRole, GetRolesWithPaginationQuery, RoleDTO>
{
    public GetRolesWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
    protected override Expression<Func<ApplicationRole, bool>> SearchStringPredicate(string searchString)
    {
        return x => x.Name.ToLower().Contains(searchString.ToLower());
    }
    protected override Expression<Func<ApplicationRole, object>> OrderByKeySelector()
    {
        return x => x.Name;
    }
}
