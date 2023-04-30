using HUTECHClassroom.Application.Faculties.Commands.CreateFaculty;
using HUTECHClassroom.Application.Faculties.Commands.DeleteFaculty;
using HUTECHClassroom.Application.Faculties.Commands.DeleteRangeFaculty;
using HUTECHClassroom.Application.Faculties.Commands.UpdateFaculty;
using HUTECHClassroom.Application.Faculties.DTOs;
using HUTECHClassroom.Application.Faculties.Queries.GetFacultiesWithPagination;
using HUTECHClassroom.Application.Faculties.Queries.GetFaculty;
using Microsoft.AspNetCore.Mvc;

namespace HUTECHClassroom.API.Controllers.Api.V1;

[ApiVersion("1.0")]
public class FacultiesController : BaseEntityApiController<FacultyDTO>
{
    [HttpGet]
    public Task<ActionResult<IEnumerable<FacultyDTO>>> Get([FromQuery] FacultyPaginationParams @params)
        => HandlePaginationQuery<GetFacultiesWithPaginationQuery, FacultyPaginationParams>(new GetFacultiesWithPaginationQuery(@params));
    [HttpGet("{id}", Name = nameof(GetFacultyDetails))]
    public Task<ActionResult<FacultyDTO>> GetFacultyDetails(Guid id)
        => HandleGetQuery(new GetFacultyQuery(id));
    [HttpPost]
    public Task<ActionResult<FacultyDTO>> Post(CreateFacultyCommand command)
        => HandleCreateCommand(command, nameof(GetFacultyDetails));
    [HttpPut("{id}")]
    public Task<IActionResult> Put(Guid id, UpdateFacultyCommand request)
        => HandleUpdateCommand(id, request);
    [HttpDelete("{id}")]
    public Task<ActionResult<FacultyDTO>> Delete(Guid id)
        => HandleDeleteCommand(new DeleteFacultyCommand(id));
    [HttpDelete]
    public Task<IActionResult> DeleteRange(IList<Guid> ids)
        => HandleDeleteRangeCommand(new DeleteRangeFacultyCommand(ids));
}
