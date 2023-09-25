namespace HUTECHClassroom.API.Controllers.Api.V1;

[ApiVersion("1.0")]
public class ProjectsController : BaseEntityApiController<ProjectDTO>
{
    [Authorize(ReadProjectPolicy)]
    [HttpGet]
    public Task<ActionResult<IEnumerable<ProjectDTO>>> Get([FromQuery] ProjectPaginationParams @params)
        => HandlePaginationQuery<GetProjectsWithPaginationQuery, ProjectPaginationParams>(new GetProjectsWithPaginationQuery(@params));
    [Authorize(ReadProjectPolicy)]
    [HttpGet("{projectId}")]
    public Task<ActionResult<ProjectDTO>> GetProjectDetails(Guid projectId)
        => HandleGetQuery(new GetProjectQuery(projectId));
    [Authorize(CreateProjectPolicy)]
    [HttpPost]
    public Task<ActionResult<ProjectDTO>> Post(CreateProjectCommand request)
        => HandleCreateCommand(request, projectId => new GetProjectQuery(projectId));
    [Authorize(UpdateProjectPolicy)]
    [HttpPut("{projectId}")]
    public Task<IActionResult> Put(Guid projectId, UpdateProjectCommand request)
        => HandleUpdateCommand(projectId, request);
    [Authorize(DeleteProjectPolicy)]
    [HttpDelete("{projectId}")]
    public Task<ActionResult<ProjectDTO>> Delete(Guid projectId)
        => HandleDeleteCommand(new DeleteProjectCommand(projectId));
    [Authorize(DeleteProjectPolicy)]
    [HttpDelete]
    public Task<IActionResult> DeleteRange(IList<Guid> projectIds)
        => HandleDeleteRangeCommand(new DeleteRangeProjectCommand(projectIds));
    [Authorize(ReadProjectPolicy)]
    [HttpGet("{projectId}/missions")]
    public async Task<ActionResult<IEnumerable<MissionDTO>>> GetMissions(Guid projectId, [FromQuery] ProjectPaginationParams @params)
        => HandlePagedList(await Mediator.Send(new GetProjectMissionsWithPaginationQuery(projectId, @params)));
    [Authorize(UpdateProjectPolicy)]
    [HttpPost("{projectId}/missions/{missionId}")]
    public async Task<ActionResult<MissionDTO>> AddMission(Guid projectId, Guid missionId)
        => Ok(await Mediator.Send(new AddMissionCommand(projectId, missionId)));
    [Authorize(UpdateProjectPolicy)]
    [HttpDelete("{projectId}/missions/{missionId}")]
    public async Task<ActionResult<MissionDTO>> RemoveMission(Guid projectId, Guid missionId)
        => Ok(await Mediator.Send(new RemoveMissionCommand(projectId, missionId)));
}
