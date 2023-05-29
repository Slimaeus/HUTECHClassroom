using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Common.Validators;
using HUTECHClassroom.Application.GroupRoles.DTOs;

namespace HUTECHClassroom.Application.GroupRoles.Queries;

public class GetGroupRolesWithPaginationQueryValidator : GetWithPaginationQueryValidator<GetGroupRolesWithPaginationQuery, GroupRoleDTO, PaginationParams>
{
    public GetGroupRolesWithPaginationQueryValidator()
    {
    }
}
