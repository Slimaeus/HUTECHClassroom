namespace HUTECHClassroom.API.Controllers.Api.V1;

[ApiVersion("1.0")]
public class CommentsController : BaseEntityApiController<CommentDTO>
{
    [Authorize(ReadCommentPolicy)]
    [HttpGet]
    public Task<ActionResult<IEnumerable<CommentDTO>>> Get([FromQuery] CommentPaginationParams @params)
        => HandlePaginationQuery<GetCommentsWithPaginationQuery, CommentPaginationParams>(new GetCommentsWithPaginationQuery(@params));

    [Authorize(ReadCommentPolicy)]
    [HttpGet("{commentId}")]
    public Task<ActionResult<CommentDTO>> GetCommentDetails(Guid commentId)
        => HandleGetQuery(new GetCommentQuery(commentId));

    [Authorize(CreateCommentPolicy)]
    [HttpPost]
    public Task<ActionResult<CommentDTO>> Post(CreateCommentCommand command)
        => HandleCreateCommand(command, commentId => new GetCommentQuery(commentId));

    [Authorize(UpdateCommentPolicy)]
    [HttpPut("{commentId}")]
    public Task<IActionResult> Put(Guid commentId, UpdateCommentCommand request)
        => HandleUpdateCommand(commentId, request);

    [Authorize(DeleteCommentPolicy)]
    [HttpDelete("{commentId}")]
    public Task<ActionResult<CommentDTO>> Delete(Guid commentId)
        => HandleDeleteCommand(new DeleteCommentCommand(commentId));

    [Authorize(DeleteCommentPolicy)]
    [HttpDelete]
    public Task<IActionResult> DeleteRange(IList<Guid> commentIds)
        => HandleDeleteRangeCommand(new DeleteRangeCommentCommand(commentIds));
}
