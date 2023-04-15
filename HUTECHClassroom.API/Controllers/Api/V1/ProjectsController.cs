using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Projects.Commands.CreateProject;
using HUTECHClassroom.Application.Projects.Commands.DeleteProject;
using HUTECHClassroom.Application.Projects.Commands.UpdateProject;
using HUTECHClassroom.Application.Projects.DTOs;
using HUTECHClassroom.Application.Projects.Queries.GetProject;
using HUTECHClassroom.Application.Projects.Queries.GetProjectsWithPagination;
using Microsoft.AspNetCore.Mvc;

namespace HUTECHClassroom.API.Controllers.Api.V1
{
    [ApiVersion("1.0")]
    public class ProjectsController : BaseEntityApiController<ProjectDTO>
    {
        [HttpGet]
        public Task<ActionResult<IEnumerable<ProjectDTO>>> Get([FromQuery] PaginationParams @params)
            => HandlePaginationQuery(new GetProjectsWithPaginationQuery(@params));
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
    }
}
