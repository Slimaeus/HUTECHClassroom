﻿using HUTECHClassroom.Application.Common.Validators;
using HUTECHClassroom.Application.Groups.DTOs;

namespace HUTECHClassroom.Application.Classrooms.Queries.GetClassroomGroupsWithPagination;

public sealed class GetClassroomGroupsWithPaginationQueryValidator : GetWithPaginationQueryValidator<GetClassroomGroupsWithPaginationQuery, GroupDTO, ClassroomPaginationParams>
{
    public GetClassroomGroupsWithPaginationQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotNull();
    }
}
