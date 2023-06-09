﻿using HUTECHClassroom.Application.Majors;
using HUTECHClassroom.Application.Majors.Commands.CreateMajor;
using HUTECHClassroom.Application.Majors.Commands.DeleteMajor;
using HUTECHClassroom.Application.Majors.Commands.DeleteRangeMajor;
using HUTECHClassroom.Application.Majors.Commands.UpdateMajor;
using HUTECHClassroom.Application.Majors.DTOs;
using HUTECHClassroom.Application.Majors.Queries.GetMajor;
using HUTECHClassroom.Application.Majors.Queries.GetMajorsWithPagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HUTECHClassroom.API.Controllers.Api.V1;

public class MajorsController : BaseEntityApiController<MajorDTO>
{
    [Authorize(ReadMajorPolicy)]
    [HttpGet]
    public Task<ActionResult<IEnumerable<MajorDTO>>> Get([FromQuery] MajorPaginationParams @params)
        => HandlePaginationQuery<GetMajorsWithPaginationQuery, MajorPaginationParams>(new GetMajorsWithPaginationQuery(@params));
    [Authorize(ReadMajorPolicy)]
    [HttpGet("{majorId}")]
    public Task<ActionResult<MajorDTO>> GetMajorDetails(Guid majorId)
        => HandleGetQuery(new GetMajorQuery(majorId));
    [Authorize(CreateMajorPolicy)]
    [HttpPost]
    public Task<ActionResult<MajorDTO>> Post(CreateMajorCommand command)
        => HandleCreateCommand(command, majorId => new GetMajorQuery(majorId));
    [Authorize(UpdateMajorPolicy)]
    [HttpPut("{majorId}")]
    public Task<IActionResult> Put(Guid majorId, UpdateMajorCommand request)
        => HandleUpdateCommand(majorId, request);
    [Authorize(DeleteMajorPolicy)]
    [HttpDelete("{majorId}")]
    public Task<ActionResult<MajorDTO>> Delete(Guid majorId)
        => HandleDeleteCommand(new DeleteMajorCommand(majorId));
    [Authorize(DeleteMajorPolicy)]
    [HttpDelete]
    public Task<IActionResult> DeleteRange(IList<Guid> majorIds)
        => HandleDeleteRangeCommand(new DeleteRangeMajorCommand(majorIds));
}