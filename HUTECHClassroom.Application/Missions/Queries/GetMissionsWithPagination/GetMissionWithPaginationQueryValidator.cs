using HUTECHClassroom.Application.Common.Validators;
using HUTECHClassroom.Application.Missions.DTOs;

namespace HUTECHClassroom.Application.Missions.Queries.GetMissionsWithPagination;

public sealed class GetMissionWithPaginationQueryValidator : GetWithPaginationQueryValidator<GetMissionsWithPaginationQuery, MissionDTO, MissionPaginationParams>
{
}
