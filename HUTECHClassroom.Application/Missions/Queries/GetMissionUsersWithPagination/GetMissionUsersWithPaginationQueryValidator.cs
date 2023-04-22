using HUTECHClassroom.Application.Common.DTOs;
using HUTECHClassroom.Application.Common.Validators;

namespace HUTECHClassroom.Application.Missions.Queries.GetMissionUsersWithPagination;

public class GetMissionUsersWithPaginationQueryValidator : GetWithPaginationQueryValidator<GetMissionUsersWithPaginationQuery, MemberDTO>
{
    public GetMissionUsersWithPaginationQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotNull();
    }
}
