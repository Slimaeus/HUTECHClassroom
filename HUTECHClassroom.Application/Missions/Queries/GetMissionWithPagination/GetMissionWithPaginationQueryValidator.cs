using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Missions.DTOs;

namespace HUTECHClassroom.Application.Missions.Queries.GetMissionWithPagination
{
    public class GetMissionWithPaginationQueryValidator : GetWithPaginationQueryValidator<GetMissionsWithPaginationQuery, MissionDTO>
    {
    }
}
