using HUTECHClassroom.Application.Faculties;
using HUTECHClassroom.Application.Faculties.Commands.CreateFaculty;
using HUTECHClassroom.Application.Faculties.Commands.DeleteFaculty;
using HUTECHClassroom.Application.Faculties.Commands.DeleteRangeFaculty;
using HUTECHClassroom.Application.Faculties.Commands.UpdateFaculty;
using HUTECHClassroom.Application.Faculties.DTOs;
using HUTECHClassroom.Application.Faculties.Queries.GetFacultiesWithPagination;
using HUTECHClassroom.Application.Faculties.Queries.GetFaculty;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HUTECHClassroom.API.Controllers.Api.V1;

[ApiVersion("1.0")]
public class FacultiesController : BaseEntityApiController<FacultyDTO>
{
    [Authorize(ReadFacultyPolicy)]
    [HttpGet]
    public Task<ActionResult<IEnumerable<FacultyDTO>>> Get([FromQuery] FacultyPaginationParams @params)
        => HandlePaginationQuery<GetFacultiesWithPaginationQuery, FacultyPaginationParams>(new GetFacultiesWithPaginationQuery(@params));
    [Authorize(ReadFacultyPolicy)]
    [HttpGet("{facultyId}")]
    public Task<ActionResult<FacultyDTO>> GetFacultyDetails(Guid facultyId)
        => HandleGetQuery(new GetFacultyQuery(facultyId));
    [Authorize(CreateFacultyPolicy)]
    [HttpPost]
    public Task<ActionResult<FacultyDTO>> Post(CreateFacultyCommand command)
        => HandleCreateCommand(command, facultyId => new GetFacultyQuery(facultyId));
    [Authorize(UpdateFacultyPolicy)]
    [HttpPut("{facultyId}")]
    public Task<IActionResult> Put(Guid facultyId, UpdateFacultyCommand request)
        => HandleUpdateCommand(facultyId, request);
    [Authorize(DeleteFacultyPolicy)]
    [HttpDelete("{facultyId}")]
    public Task<ActionResult<FacultyDTO>> Delete(Guid facultyId)
        => HandleDeleteCommand(new DeleteFacultyCommand(facultyId));
    [Authorize(DeleteFacultyPolicy)]
    [HttpDelete]
    public Task<IActionResult> DeleteRange(IList<Guid> facultyId)
        => HandleDeleteRangeCommand(new DeleteRangeFacultyCommand(facultyId));
}
