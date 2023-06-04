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
    [HttpGet("{projectId}")]
    public Task<ActionResult<ProjectDTO>> GetProjectDetails(Guid projectId)
        => HandleGetQuery(new GetProjectQuery(projectId));
    [HttpPost]
    public Task<ActionResult<ProjectDTO>> Post(CreateProjectCommand request)
        => HandleCreateCommand(request, projectId => new GetProjectQuery(projectId));
    [HttpPut("{projectId}")]
    public Task<IActionResult> Put(Guid projectId, UpdateProjectCommand request)
        => HandleUpdateCommand(projectId, request);
    [HttpDelete("{projectId}")]
    public Task<ActionResult<ProjectDTO>> Delete(Guid projectId)
        => HandleDeleteCommand(new DeleteProjectCommand(projectId));
    [HttpDelete]
    public Task<IActionResult> DeleteRange(IList<Guid> projectIds)
        => HandleDeleteRangeCommand(new DeleteRangeProjectCommand(projectIds));
    [HttpGet("{projectId}/missions")]
    public async Task<ActionResult<IEnumerable<MissionDTO>>> GetMissions(Guid projectId, [FromQuery] PaginationParams @params)
        => HandlePagedList(await Mediator.Send(new GetProjectMissionsWithPaginationQuery(projectId, @params)));
    [HttpPost("{projectId}/missions/{missionId}")]
    public async Task<ActionResult<MissionDTO>> AddMission(Guid projectId, Guid missionId)
        => Ok(await Mediator.Send(new AddMissionCommand(projectId, missionId)));
    [HttpDelete("{projectId}/missions/{missionId}")]
    public async Task<ActionResult<MissionDTO>> RemoveMission(Guid projectId, Guid missionId)
        => Ok(await Mediator.Send(new RemoveMissionCommand(projectId, missionId)));
}
