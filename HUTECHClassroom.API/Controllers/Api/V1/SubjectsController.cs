using HUTECHClassroom.Application.Subjects;
using HUTECHClassroom.Application.Subjects.Commands.CreateSubject;
using HUTECHClassroom.Application.Subjects.Commands.DeleteRangeSubject;
using HUTECHClassroom.Application.Subjects.Commands.DeleteSubject;
using HUTECHClassroom.Application.Subjects.Commands.UpdateSubject;
using HUTECHClassroom.Application.Subjects.DTOs;
using HUTECHClassroom.Application.Subjects.Queries.GetSubject;
using HUTECHClassroom.Application.Subjects.Queries.GetSubjectsWithPagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HUTECHClassroom.API.Controllers.Api.V1;

public class SubjectsController : BaseEntityApiController<SubjectDTO>
{
    [Authorize(ReadSubjectPolicy)]
    [HttpGet]
    public Task<ActionResult<IEnumerable<SubjectDTO>>> Get([FromQuery] SubjectPaginationParams @params)
        => HandlePaginationQuery<GetSubjectsWithPaginationQuery, SubjectPaginationParams>(new GetSubjectsWithPaginationQuery(@params));
    [Authorize(ReadSubjectPolicy)]
    [HttpGet("{subjectId}")]
    public Task<ActionResult<SubjectDTO>> GetSubjectDetails(Guid subjectId)
        => HandleGetQuery(new GetSubjectQuery(subjectId));
    [Authorize(CreateSubjectPolicy)]
    [HttpPost]
    public Task<ActionResult<SubjectDTO>> Post(CreateSubjectCommand command)
        => HandleCreateCommand(command, subjectId => new GetSubjectQuery(subjectId));
    [Authorize(UpdateSubjectPolicy)]
    [HttpPut("{subjectId}")]
    public Task<IActionResult> Put(Guid subjectId, UpdateSubjectCommand request)
        => HandleUpdateCommand(subjectId, request);
    [Authorize(DeleteSubjectPolicy)]
    [HttpDelete("{subjectId}")]
    public Task<ActionResult<SubjectDTO>> Delete(Guid subjectId)
        => HandleDeleteCommand(new DeleteSubjectCommand(subjectId));
    [Authorize(DeleteSubjectPolicy)]
    [HttpDelete]
    public Task<IActionResult> DeleteRange(IList<Guid> subjectIds)
        => HandleDeleteRangeCommand(new DeleteRangeSubjectCommand(subjectIds));
}
