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
    [HttpGet("{id}", Name = nameof(GetMajorDetails))]
    public Task<ActionResult<MajorDTO>> GetMajorDetails(Guid id)
        => HandleGetQuery(new GetMajorQuery(id));
    [HttpPost]
    public Task<ActionResult<MajorDTO>> Post(CreateMajorCommand command)
        => HandleCreateCommand(command, nameof(GetMajorDetails));
    [HttpPut("{id}")]
    public Task<IActionResult> Put(Guid id, UpdateMajorCommand request)
        => HandleUpdateCommand(id, request);
    [HttpDelete("{id}")]
    public Task<ActionResult<MajorDTO>> Delete(Guid id)
        => HandleDeleteCommand(new DeleteMajorCommand(id));
    [HttpDelete]
    public Task<IActionResult> DeleteRange(IList<Guid> ids)
        => HandleDeleteRangeCommand(new DeleteRangeMajorCommand(ids));
}