using HUTECHClassroom.Application.Common.DTOs;
using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Missions;
using HUTECHClassroom.Application.Missions.Commands.AddMissionUser;
using HUTECHClassroom.Application.Missions.Commands.AddRangeMissionUser;
using HUTECHClassroom.Application.Missions.Commands.CreateMission;
using HUTECHClassroom.Application.Missions.Commands.DeleteMission;
using HUTECHClassroom.Application.Missions.Commands.DeleteRangeMission;
using HUTECHClassroom.Application.Missions.Commands.RemoveMissionUser;
using HUTECHClassroom.Application.Missions.Commands.RemoveRangeMissionUser;
using HUTECHClassroom.Application.Missions.Commands.UpdateMission;
using HUTECHClassroom.Application.Missions.DTOs;
using HUTECHClassroom.Application.Missions.Queries.GetMission;
using HUTECHClassroom.Application.Missions.Queries.GetMissionsWithPagination;
using HUTECHClassroom.Application.Missions.Queries.GetMissionUsersWithPagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HUTECHClassroom.API.Controllers.Api.V1;

[ApiVersion("1.0")]
public class MissionsController : BaseEntityApiController<MissionDTO>
{
    [Authorize(ReadMissionPolicy)]
    [HttpGet]
    public Task<ActionResult<IEnumerable<MissionDTO>>> Get([FromQuery] MissionPaginationParams @params)
        => HandlePaginationQuery<GetMissionsWithPaginationQuery, MissionPaginationParams>(new GetMissionsWithPaginationQuery(@params));
    [Authorize(ReadMissionPolicy)]
    [HttpGet("{missionId}")]
    public Task<ActionResult<MissionDTO>> GetDetails(Guid missionId)
        => HandleGetQuery(new GetMissionQuery(missionId));
    [Authorize(CreateMissionPolicy)]
    [HttpPost]
    public Task<ActionResult<MissionDTO>> Post(CreateMissionCommand request)
        => HandleCreateCommand(request, missionId => new GetMissionQuery(missionId));
    [Authorize(UpdateMissionPolicy)]
    [HttpPut("{missionId}")]
    public Task<IActionResult> Put(Guid missionId, UpdateMissionCommand request)
        => HandleUpdateCommand(missionId, request);
    [Authorize(DeleteMissionPolicy)]
    [HttpDelete("{missionId}")]
    public Task<ActionResult<MissionDTO>> Delete(Guid missionId)
        => HandleDeleteCommand(new DeleteMissionCommand(missionId));
    [Authorize(DeleteMissionPolicy)]
    [HttpDelete]
    public Task<IActionResult> DeleteRange(IList<Guid> missionIds)
        => HandleDeleteRangeCommand(new DeleteRangeMissionCommand(missionIds));
    [Authorize(ReadMissionPolicy)]
    [HttpGet("{missionId}/members")]
    public async Task<ActionResult<IEnumerable<MemberDTO>>> GetMembers(Guid missionId, [FromQuery] PaginationParams @params)
        => HandlePagedList(await Mediator.Send(new GetMissionUsersWithPaginationQuery(missionId, @params)));
    [Authorize(UpdateMissionPolicy)]
    [HttpPost("{missionId}/members/{userId}")]
    public async Task<IActionResult> AddMember(Guid missionId, Guid userId)
        => Ok(await Mediator.Send(new AddMissionUserCommand(missionId, userId)));
    [Authorize(UpdateMissionPolicy)]
    [HttpDelete("{missionId}/members/{userId}")]
    public async Task<IActionResult> RemoveMember(Guid missionId, Guid userId)
        => Ok(await Mediator.Send(new RemoveMissionUserCommand(missionId, userId)));
    [Authorize(Policy = UpdateMissionPolicy)]
    [HttpPost("{missionId}/members/add")]
    public async Task<IActionResult> AddMembers(Guid missionId, IList<Guid> userIds)
        => Ok(await Mediator.Send(new AddRangeMissionUserCommand(missionId, userIds)));
    [Authorize(Policy = UpdateMissionPolicy)]
    [HttpPost("{missionId}/members/remove")]
    public async Task<IActionResult> RemoveMembers(Guid missionId, IList<Guid> userIds)
        => Ok(await Mediator.Send(new RemoveRangeMissionUserCommand(missionId, userIds)));
}
