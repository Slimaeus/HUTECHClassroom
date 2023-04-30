﻿using HUTECHClassroom.Application.Classrooms.DTOs;
using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Common.Validators;

namespace HUTECHClassroom.Application.Classrooms.Queries.GetClassroomGroupsWithPagination;

public class GetClassroomGroupsWithPaginationQueryValidator : GetWithPaginationQueryValidator<GetClassroomGroupsWithPaginationQuery, ClassroomGroupDTO, PaginationParams>
{
    public GetClassroomGroupsWithPaginationQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotNull();
    }
}
