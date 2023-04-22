using HUTECHClassroom.Application.Common.DTOs;
using HUTECHClassroom.Application.Common.Models;
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
using HUTECHClassroom.Application.Missions.Queries.GetMissionUser;
using HUTECHClassroom.Application.Missions.Queries.GetMissionUsersWithPagination;
using HUTECHClassroom.Application.Projects.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace HUTECHClassroom.API.Controllers.Api.V1;

[ApiVersion("1.0")]
public class MissionsController : BaseEntityApiController<MissionDTO>
{
    [HttpGet]
    public Task<ActionResult<IEnumerable<MissionDTO>>> Get([FromQuery] PaginationParams @params)
        => HandlePaginationQuery(new GetMissionsWithPaginationQuery(@params));
    [HttpGet("{id}", Name = nameof(GetDetails))]
    public Task<ActionResult<MissionDTO>> GetDetails(Guid id)
        => HandleGetQuery(new GetMissionQuery(id));
    [HttpPost]
    public Task<ActionResult<MissionDTO>> Post(CreateMissionCommand request)
        => HandleCreateCommand(request, nameof(GetDetails));
    [HttpPut("{id}")]
    public Task<IActionResult> Put(Guid id, UpdateMissionCommand request)
        => HandleUpdateCommand(id, request);
    [HttpDelete("{id}")]
    public Task<ActionResult<MissionDTO>> Delete(Guid id)
        => HandleDeleteCommand(new DeleteMissionCommand(id));
    [HttpDelete]
    public Task<IActionResult> DeleteRange(IList<Guid> ids)
        => HandleDeleteRangeCommand(new DeleteRangeMissionCommand(ids));
    [HttpGet("{id}/members")]
    public async Task<ActionResult<IEnumerable<MemberDTO>>> GetMembers(Guid id, [FromQuery] PaginationParams @params)
        => HandlePagedList(await Mediator.Send(new GetMissionUsersWithPaginationQuery(id, @params)));
    // Same as Get User Profile
    [HttpGet("{id}/members/{userName}")]
    public async Task<ActionResult<MemberDTO>> GetMember(Guid id, string userName)
        => Ok(await Mediator.Send(new GetMissionUserQuery(id, userName)));
    [HttpPost("{id}/members/{userName}")]
    public async Task<IActionResult> AddMember(Guid id, string userName)
        => Ok(await Mediator.Send(new AddMissionUserCommand(id, userName)));
    [HttpDelete("{id}/members/{userName}")]
    public async Task<IActionResult> RemoveMember(Guid id, string userName)
        => Ok(await Mediator.Send(new RemoveMissionUserCommand(id, userName)));
    [HttpGet("{id}/project")]
    public async Task<ActionResult<ProjectDTO>> GetProject(Guid id)
        => Ok(await Mediator.Send(new GetMissionProjectQuery(id)));
}
