using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Common.Validators;
using HUTECHClassroom.Application.Groups.DTOs;

namespace HUTECHClassroom.Application.Users.Queries.GetUserGroupsWithPagination;

public sealed class GetUserGroupsWithPaginationQueryValidator : GetWithPaginationQueryValidator<GetUserGroupsWithPaginationQuery, GroupDTO, PaginationParams>
{
}
