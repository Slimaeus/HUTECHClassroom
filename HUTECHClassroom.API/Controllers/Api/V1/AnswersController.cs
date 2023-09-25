namespace HUTECHClassroom.API.Controllers.Api.V1;

[ApiVersion("1.0")]
public class AnswersController : BaseEntityApiController<AnswerDTO>
{
    [Authorize(ReadAnswerPolicy)]
    [HttpGet]
    public Task<ActionResult<IEnumerable<AnswerDTO>>> Get([FromQuery] AnswerPaginationParams @params)
        => HandlePaginationQuery<GetAnswersWithPaginationQuery, AnswerPaginationParams>(new GetAnswersWithPaginationQuery(@params));

    [Authorize(ReadAnswerPolicy)]
    [HttpGet("{answerId}")]
    public Task<ActionResult<AnswerDTO>> GetAnswerDetails(Guid answerId)
        => HandleGetQuery(new GetAnswerQuery(answerId));

    [Authorize(CreateAnswerPolicy)]
    [HttpPost]
    public Task<ActionResult<AnswerDTO>> Post(CreateAnswerCommand command)
        => HandleCreateCommand(command, answerId => new GetAnswerQuery(answerId));

    [Authorize(UpdateAnswerPolicy)]
    [HttpPut("{answerId}")]
    public Task<IActionResult> Put(Guid answerId, UpdateAnswerCommand request)
        => HandleUpdateCommand(answerId, request);

    [Authorize(DeleteAnswerPolicy)]
    [HttpDelete("{answerId}")]
    public Task<ActionResult<AnswerDTO>> Delete(Guid answerId)
        => HandleDeleteCommand(new DeleteAnswerCommand(answerId));

    [Authorize(DeleteAnswerPolicy)]
    [HttpDelete]
    public Task<IActionResult> DeleteRange(IList<Guid> answerIds)
        => HandleDeleteRangeCommand(new DeleteRangeAnswerCommand(answerIds));
}
