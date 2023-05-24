using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Missions.DTOs;
using HUTECHClassroom.Application.Projects;
using HUTECHClassroom.Application.Projects.Commands.AddMission;
using HUTECHClassroom.Application.Projects.Commands.CreateProject;
using HUTECHClassroom.Application.Projects.Commands.DeleteProject;
using HUTECHClassroom.Application.Projects.Commands.DeleteRangeProject;
using HUTECHClassroom.Application.Projects.Commands.RemoveMission;
using HUTECHClassroom.Application.Projects.Commands.UpdateProject;
using HUTECHClassroom.Application.Projects.DTOs;
using HUTECHClassroom.Application.Projects.Queries.GetProject;
using HUTECHClassroom.Application.Projects.Queries.GetProjectMission;
using HUTECHClassroom.Application.Projects.Queries.GetProjectMissionsWithPagination;
using HUTECHClassroom.Application.Projects.Queries.GetProjectsWithPagination;
using Microsoft.AspNetCore.Mvc;

namespace HUTECHClassroom.API.Controllers.Api.V1;

[ApiVersion("1.0")]
public class ProjectsController : BaseEntityApiController<ProjectDTO>
{
    [HttpGet]
    public Task<ActionResult<IEnumerable<ProjectDTO>>> Get([FromQuery] ProjectPaginationParams @params)
        => HandlePaginationQuery<GetProjectsWithPaginationQuery, ProjectPaginationParams>(new GetProjectsWithPaginationQuery(@params));
    [HttpGet("{id}", Name = nameof(GetProjectDetails))]
    public Task<ActionResult<ProjectDTO>> GetProjectDetails(Guid id)
        => HandleGetQuery(new GetProjectQuery(id));
    [HttpPost]
    public Task<ActionResult<ProjectDTO>> Post(CreateProjectCommand request)
        => HandleCreateCommand(request, nameof(GetProjectDetails));
    [HttpPut("{id}")]
    public Task<IActionResult> Put(Guid id, UpdateProjectCommand request)
        => HandleUpdateCommand(id, request);
    [HttpDelete("{id}")]
    public Task<ActionResult<ProjectDTO>> Delete(Guid id)
        => HandleDeleteCommand(new DeleteProjectCommand(id));
    [HttpDelete]
    public Task<IActionResult> DeleteRange(IList<Guid> ids)
        => HandleDeleteRangeCommand(new DeleteRangeProjectCommand(ids));
    [HttpGet("{id}/missions")]
    public async Task<ActionResult<IEnumerable<MissionDTO>>> GetMissions(Guid id, [FromQuery] PaginationParams @params)
        => HandlePagedList(await Mediator.Send(new GetProjectMissionsWithPaginationQuery(id, @params)));
    // Same as Get Mission
    [HttpGet("{id}/missions/{missionId}")]
    public async Task<ActionResult<MissionDTO>> GetMission(Guid id, Guid missionId)
        => Ok(await Mediator.Send(new GetProjectMissionQuery(id, missionId)));
    [HttpPost("{id}/missions/{missionId}")]
    public async Task<ActionResult<MissionDTO>> AddMission(Guid id, Guid missionId)
        => Ok(await Mediator.Send(new AddMissionCommand(id, missionId)));
    [HttpDelete("{id}/missions/{missionId}")]
    public async Task<ActionResult<MissionDTO>> RemoveMission(Guid id, Guid missionId)
        => Ok(await Mediator.Send(new RemoveMissionCommand(id, missionId)));
}
