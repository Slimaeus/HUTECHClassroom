using HUTECHClassroom.Application.Common.Validators;
using HUTECHClassroom.Application.Groups.DTOs;

namespace HUTECHClassroom.Application.Groups.Queries.GetGroupsWithPagination;

public sealed class GetGroupsWithPaginationQueryValidator : GetWithPaginationQueryValidator<GetGroupsWithPaginationQuery, GroupDTO, GroupPaginationParams>
{
    public GetGroupsWithPaginationQueryValidator()
    {
    }
}
