using HUTECHClassroom.Application.Majors;
using HUTECHClassroom.Application.Majors.Commands.CreateMajor;
using HUTECHClassroom.Application.Majors.Commands.DeleteMajor;
using HUTECHClassroom.Application.Majors.Commands.DeleteRangeMajor;
using HUTECHClassroom.Application.Majors.Commands.UpdateMajor;
using HUTECHClassroom.Application.Majors.DTOs;
using HUTECHClassroom.Application.Majors.Queries.GetMajor;
using HUTECHClassroom.Application.Majors.Queries.GetMajorsWithPagination;
using Microsoft.AspNetCore.Mvc;

namespace HUTECHClassroom.API.Controllers.Api.V1;

public class MajorsController : BaseEntityApiController<MajorDTO>
{
    [HttpGet]
    public Task<ActionResult<IEnumerable<MajorDTO>>> Get([FromQuery] MajorPaginationParams @params)
        => HandlePaginationQuery<GetMajorsWithPaginationQuery, MajorPaginationParams>(new GetMajorsWithPaginationQuery(@params));
    [HttpGet("{majorId}")]
    public Task<ActionResult<MajorDTO>> GetMajorDetails(Guid majorId)
        => HandleGetQuery(new GetMajorQuery(majorId));
    [HttpPost]
    public Task<ActionResult<MajorDTO>> Post(CreateMajorCommand command)
        => HandleCreateCommand(command, majorId => new GetMajorQuery(majorId));
    [HttpPut("{majorId}")]
    public Task<IActionResult> Put(Guid majorId, UpdateMajorCommand request)
        => HandleUpdateCommand(majorId, request);
    [HttpDelete("{majorId}")]
    public Task<ActionResult<MajorDTO>> Delete(Guid majorId)
        => HandleDeleteCommand(new DeleteMajorCommand(majorId));
    [HttpDelete]
    public Task<IActionResult> DeleteRange(IList<Guid> majorIds)
        => HandleDeleteRangeCommand(new DeleteRangeMajorCommand(majorIds));
}