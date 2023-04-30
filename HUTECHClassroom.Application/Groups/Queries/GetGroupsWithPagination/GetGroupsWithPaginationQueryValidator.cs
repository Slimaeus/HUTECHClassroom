using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Common.Validators;
using HUTECHClassroom.Application.Groups.DTOs;

namespace HUTECHClassroom.Application.Groups.Queries.GetGroupsWithPagination;

public class GetGroupsWithPaginationQueryValidator : GetWithPaginationQueryValidator<GetGroupsWithPaginationQuery, GroupDTO, PaginationParams>
{
    public GetGroupsWithPaginationQueryValidator()
    {
    }
}
