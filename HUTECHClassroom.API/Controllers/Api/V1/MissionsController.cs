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
using HUTECHClassroom.Application.Missions.Queries.GetMissionProject;
using HUTECHClassroom.Application.Missions.Queries.GetMissionsWithPagination;
using HUTECHClassroom.Application.Missions.Queries.GetMissionUsersWithPagination;
using HUTECHClassroom.Application.Projects.DTOs;
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
    [HttpGet("{id}", Name = nameof(GetDetails))]
    public Task<ActionResult<MissionDTO>> GetDetails(Guid id)
        => HandleGetQuery(new GetMissionQuery(id));
    //[Authorize(Policy = CreateMissionPolicy)]
    [HttpPost]
    public Task<ActionResult<MissionDTO>> Post(CreateMissionCommand request)
        => HandleCreateCommand(request, nameof(GetDetails), id => new GetMissionQuery(id));
    //[Authorize(Policy = UpdateMissionPolicy)]
    [HttpPut("{id}")]
    public Task<IActionResult> Put(Guid id, UpdateMissionCommand request)
        => HandleUpdateCommand(id, request);
    //[Authorize(Policy = DeleteMissionPolicy)]
    [HttpDelete("{id}")]
    public Task<ActionResult<MissionDTO>> Delete(Guid id)
        => HandleDeleteCommand(new DeleteMissionCommand(id));
    //[Authorize(Policy = DeleteMissionPolicy)]
    [HttpDelete]
    public Task<IActionResult> DeleteRange(IList<Guid> ids)
        => HandleDeleteRangeCommand(new DeleteRangeMissionCommand(ids));
    //[Authorize(Policy = ReadMissionPolicy)]
    [HttpGet("{id}/members")]
    public async Task<ActionResult<IEnumerable<MemberDTO>>> GetMembers(Guid id, [FromQuery] PaginationParams @params)
        => HandlePagedList(await Mediator.Send(new GetMissionUsersWithPaginationQuery(id, @params)));
    //[Authorize(Policy = UpdateMissionPolicy)]
    [HttpPost("{id}/members/{userId}")]
    public async Task<IActionResult> AddMember(Guid id, Guid userId)
        => Ok(await Mediator.Send(new AddMissionUserCommand(id, userId)));
    //[Authorize(Policy = UpdateMissionPolicy)]
    [HttpDelete("{id}/members/{userId}")]
    public async Task<IActionResult> RemoveMember(Guid id, Guid userId)
        => Ok(await Mediator.Send(new RemoveMissionUserCommand(id, userId)));
    //[Authorize(Policy = ReadMissionPolicy)]
    [HttpGet("{id}/project")]
    public async Task<ActionResult<ProjectDTO>> GetProject(Guid id)
        => Ok(await Mediator.Send(new GetMissionProjectQuery(id)));
}
