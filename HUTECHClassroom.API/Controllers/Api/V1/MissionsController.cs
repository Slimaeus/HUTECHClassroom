using HUTECHClassroom.Application.Common.DTOs;
using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Missions;
using HUTECHClassroom.Application.Missions.Commands.AddMissionUser;
using HUTECHClassroom.Application.Missions.Commands.CreateMission;
using HUTECHClassroom.Application.Missions.Commands.DeleteMission;
using HUTECHClassroom.Application.Missions.Commands.DeleteRangeMission;
using HUTECHClassroom.Application.Missions.Commands.RemoveMissionUser;
using HUTECHClassroom.Application.Missions.Commands.UpdateMission;
using HUTECHClassroom.Application.Missions.DTOs;
using HUTECHClassroom.Application.Missions.Queries.GetMission;
using HUTECHClassroom.Application.Missions.Queries.GetMissionsWithPagination;
using HUTECHClassroom.Application.Missions.Queries.GetMissionUsersWithPagination;
using Microsoft.AspNetCore.Mvc;

namespace HUTECHClassroom.API.Controllers.Api.V1;

[ApiVersion("1.0")]
public class MissionsController : BaseEntityApiController<MissionDTO>
{
    //[Authorize(Policy = ReadMissionPolicy)]
    [HttpGet]
    public Task<ActionResult<IEnumerable<MissionDTO>>> Get([FromQuery] MissionPaginationParams @params)
        => HandlePaginationQuery<GetMissionsWithPaginationQuery, MissionPaginationParams>(new GetMissionsWithPaginationQuery(@params));
    //[Authorize(Policy = ReadMissionPolicy)]
    [HttpGet("{missionId}", Name = nameof(GetDetails))]
    public Task<ActionResult<MissionDTO>> GetDetails(Guid missionId)
        => HandleGetQuery(new GetMissionQuery(missionId));
    //[Authorize(Policy = CreateMissionPolicy)]
    [HttpPost]
    public Task<ActionResult<MissionDTO>> Post(CreateMissionCommand request)
        => HandleCreateCommand(request, nameof(GetDetails), missionId => new GetMissionQuery(missionId));
    //[Authorize(Policy = UpdateMissionPolicy)]
    [HttpPut("{missionId}")]
    public Task<IActionResult> Put(Guid missionId, UpdateMissionCommand request)
        => HandleUpdateCommand(missionId, request);
    //[Authorize(Policy = DeleteMissionPolicy)]
    [HttpDelete("{missionId}")]
    public Task<ActionResult<MissionDTO>> Delete(Guid missionId)
        => HandleDeleteCommand(new DeleteMissionCommand(missionId));
    //[Authorize(Policy = DeleteMissionPolicy)]
    [HttpDelete]
    public Task<IActionResult> DeleteRange(IList<Guid> missionIds)
        => HandleDeleteRangeCommand(new DeleteRangeMissionCommand(missionIds));
    //[Authorize(Policy = ReadMissionPolicy)]
    [HttpGet("{missionId}/members")]
    public async Task<ActionResult<IEnumerable<MemberDTO>>> GetMembers(Guid missionId, [FromQuery] PaginationParams @params)
        => HandlePagedList(await Mediator.Send(new GetMissionUsersWithPaginationQuery(missionId, @params)));
    //[Authorize(Policy = UpdateMissionPolicy)]
    [HttpPost("{missionId}/members/{userId}")]
    public async Task<IActionResult> AddMember(Guid missionId, Guid userId)
        => Ok(await Mediator.Send(new AddMissionUserCommand(missionId, userId)));
    //[Authorize(Policy = UpdateMissionPolicy)]
    [HttpDelete("{missionId}/members/{userId}")]
    public async Task<IActionResult> RemoveMember(Guid missionId, Guid userId)
        => Ok(await Mediator.Send(new RemoveMissionUserCommand(missionId, userId)));
}
