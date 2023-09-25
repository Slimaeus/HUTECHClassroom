namespace HUTECHClassroom.API.Controllers.Api.V1;

[ApiVersion("1.0")]
public class PostsController : BaseEntityApiController<PostDTO>
{
    [Authorize(ReadPostPolicy)]
    [HttpGet]
    public Task<ActionResult<IEnumerable<PostDTO>>> Get([FromQuery] PostPaginationParams @params)
        => HandlePaginationQuery<GetPostsWithPaginationQuery, PostPaginationParams>(new GetPostsWithPaginationQuery(@params));
    [Authorize(ReadPostPolicy)]
    [HttpGet("{postId}")]
    public Task<ActionResult<PostDTO>> GetPostDetails(Guid postId)
        => HandleGetQuery(new GetPostQuery(postId));
    [Authorize(CreatePostPolicy)]
    [HttpPost]
    public Task<ActionResult<PostDTO>> Post(CreatePostCommand command)
        => HandleCreateCommand(command, postId => new GetPostQuery(postId));
    [Authorize(UpdatePostPolicy)]
    [HttpPut("{postId}")]
    public Task<IActionResult> Put(Guid postId, UpdatePostCommand request)
        => HandleUpdateCommand(postId, request);
    [Authorize(DeletePostPolicy)]
    [HttpDelete("{postId}")]
    public Task<ActionResult<PostDTO>> Delete(Guid postId)
        => HandleDeleteCommand(new DeletePostCommand(postId));
    [Authorize(DeletePostPolicy)]
    [HttpDelete]
    public Task<IActionResult> DeleteRange(IList<Guid> postIds)
        => HandleDeleteRangeCommand(new DeleteRangePostCommand(postIds));
    [Authorize(ReadPostPolicy)]
    [HttpGet("{postId}/comments")]
    public async Task<ActionResult<IEnumerable<CommentDTO>>> GetComments(Guid postId, [FromQuery] PostPaginationParams @params)
        => HandlePagedList(await Mediator.Send(new GetPostCommentsWithPaginationQuery(postId, @params)));
}
