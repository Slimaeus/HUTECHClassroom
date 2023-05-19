using HUTECHClassroom.Application.Subjects;
using HUTECHClassroom.Application.Subjects.Commands.CreateSubject;
using HUTECHClassroom.Application.Subjects.Commands.DeleteRangeSubject;
using HUTECHClassroom.Application.Subjects.Commands.DeleteSubject;
using HUTECHClassroom.Application.Subjects.Commands.UpdateSubject;
using HUTECHClassroom.Application.Subjects.DTOs;
using HUTECHClassroom.Application.Subjects.Queries.GetSubject;
using HUTECHClassroom.Application.Subjects.Queries.GetSubjectsWithPagination;
using Microsoft.AspNetCore.Mvc;

namespace HUTECHClassroom.API.Controllers.Api.V1;

public class SubjectsController : BaseEntityApiController<SubjectDTO>
{
    [HttpGet]
    public Task<ActionResult<IEnumerable<SubjectDTO>>> Get([FromQuery] SubjectPaginationParams @params)
        => HandlePaginationQuery<GetSubjectsWithPaginationQuery, SubjectPaginationParams>(new GetSubjectsWithPaginationQuery(@params));
    [HttpGet("{id}", Name = nameof(GetSubjectDetails))]
    public Task<ActionResult<SubjectDTO>> GetSubjectDetails(Guid id)
        => HandleGetQuery(new GetSubjectQuery(id));
    [HttpPost]
    public Task<ActionResult<SubjectDTO>> Post(CreateSubjectCommand command)
        => HandleCreateCommand(command, nameof(GetSubjectDetails));
    [HttpPut("{id}")]
    public Task<IActionResult> Put(Guid id, UpdateSubjectCommand request)
        => HandleUpdateCommand(id, request);
    [HttpDelete("{id}")]
    public Task<ActionResult<SubjectDTO>> Delete(Guid id)
        => HandleDeleteCommand(new DeleteSubjectCommand(id));
    [HttpDelete]
    public Task<IActionResult> DeleteRange(IList<Guid> ids)
        => HandleDeleteRangeCommand(new DeleteRangeSubjectCommand(ids));
}
