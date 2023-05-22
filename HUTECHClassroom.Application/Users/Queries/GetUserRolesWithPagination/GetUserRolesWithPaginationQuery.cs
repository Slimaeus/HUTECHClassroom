﻿using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Roles.DTOs;
using HUTECHClassroom.Domain.Interfaces;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Users.Queries.GetUserRolesWithPagination;

public record GetUserRolesWithPaginationQuery(PaginationParams Params) : GetWithPaginationQuery<RoleDTO, PaginationParams>(Params);
public class GetUserRolesWithPaginationQueryHandler : GetWithPaginationQueryHandler<ApplicationRole, GetUserRolesWithPaginationQuery, RoleDTO, PaginationParams>
{
    private readonly IUserAccessor _userAccessor;

    public GetUserRolesWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserAccessor userAccessor) : base(unitOfWork, mapper)
    {
        _userAccessor = userAccessor;
    }
    protected override Expression<Func<ApplicationRole, bool>> FilterPredicate(GetUserRolesWithPaginationQuery query)
        => x => x.ApplicationUserRoles.Any(y => y.UserId == _userAccessor.Id);
}
