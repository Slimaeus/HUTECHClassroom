using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Common.Validators;
using HUTECHClassroom.Application.Missions.DTOs;

namespace HUTECHClassroom.Application.Users.Queries.GetUserMissionsWithPagination;

public class GetUserMissionsWithPaginationQueryValidator : GetWithPaginationQueryValidator<GetUserMissionsWithPaginationQuery, MissionDTO, PaginationParams>
{
}
