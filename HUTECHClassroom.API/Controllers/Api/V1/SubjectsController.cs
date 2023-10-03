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
        => HandleCreateCommand<CreateSubjectCommand, GetSubjectQuery>(command);

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
