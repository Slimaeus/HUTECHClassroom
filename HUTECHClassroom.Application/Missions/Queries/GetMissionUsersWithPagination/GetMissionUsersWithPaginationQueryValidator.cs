﻿using HUTECHClassroom.Application.Common.DTOs;
using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Common.Validators;

namespace HUTECHClassroom.Application.Missions.Queries.GetMissionUsersWithPagination;

public sealed class GetMissionUsersWithPaginationQueryValidator : GetWithPaginationQueryValidator<GetMissionUsersWithPaginationQuery, MemberDTO, PaginationParams>
{
    public GetMissionUsersWithPaginationQueryValidator() : base()
    {
        RuleFor(x => x.Id).NotEmpty().NotNull();
    }
}
