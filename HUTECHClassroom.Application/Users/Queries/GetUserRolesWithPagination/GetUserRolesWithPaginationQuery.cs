using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Roles.DTOs;
using HUTECHClassroom.Infrastructure.Services;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Users.Queries.GetUserRolesWithPagination;

public record GetUserRolesWithPaginationQuery(PaginationParams Params) : GetWithPaginationQuery<RoleDTO>(Params);
public class GetUserRolesWithPaginationQueryHandler : GetWithPaginationQueryHandler<ApplicationRole, GetUserRolesWithPaginationQuery, RoleDTO>
{
    private readonly IUserAccessor _userAccessor;

    public GetUserRolesWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserAccessor userAccessor) : base(unitOfWork, mapper)
    {
        _userAccessor = userAccessor;
    }
    protected override Expression<Func<ApplicationRole, bool>> FilterPredicate(GetUserRolesWithPaginationQuery query)
    {
        return x => x.ApplicationUserRoles.Any(y => y.User.UserName == _userAccessor.UserName);
    }
}
