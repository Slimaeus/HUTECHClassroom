using HUTECHClassroom.Application.Comments;
using HUTECHClassroom.Application.Comments.DTOs;
using HUTECHClassroom.Application.Posts;
using HUTECHClassroom.Application.Posts.Commands.CreatePost;
using HUTECHClassroom.Application.Posts.Commands.DeletePost;
using HUTECHClassroom.Application.Posts.Commands.DeleteRangePost;
using HUTECHClassroom.Application.Posts.Commands.UpdatePost;
using HUTECHClassroom.Application.Posts.DTOs;
using HUTECHClassroom.Application.Posts.Queries.GetPost;
using HUTECHClassroom.Application.Posts.Queries.GetPostCommentsWithPagination;
using HUTECHClassroom.Application.Posts.Queries.GetPostsWithPagination;
using Microsoft.AspNetCore.Mvc;

namespace HUTECHClassroom.API.Controllers.Api.V1;

[ApiVersion("1.0")]
public class PostsController : BaseEntityApiController<PostDTO>
{
    [HttpGet]
    public Task<ActionResult<IEnumerable<PostDTO>>> Get([FromQuery] PostPaginationParams @params)
        => HandlePaginationQuery<GetPostsWithPaginationQuery, PostPaginationParams>(new GetPostsWithPaginationQuery(@params));
    [HttpGet("{postId}", Name = nameof(GetPostDetails))]
    public Task<ActionResult<PostDTO>> GetPostDetails(Guid postId)
        => HandleGetQuery(new GetPostQuery(postId));
    [HttpPost]
    public Task<ActionResult<PostDTO>> Post(CreatePostCommand command)
        => HandleCreateCommand(command, nameof(GetPostDetails), postId => new GetPostQuery(postId));
    [HttpPut("{postId}")]
    public Task<IActionResult> Put(Guid postId, UpdatePostCommand request)
        => HandleUpdateCommand(postId, request);
    [HttpDelete("{postId}")]
    public Task<ActionResult<PostDTO>> Delete(Guid postId)
        => HandleDeleteCommand(new DeletePostCommand(postId));
    [HttpDelete]
    public Task<IActionResult> DeleteRange(IList<Guid> postIds)
        => HandleDeleteRangeCommand(new DeleteRangePostCommand(postIds));
    [HttpGet("{postId}/comments")]
    public async Task<ActionResult<IEnumerable<CommentDTO>>> GetComments(Guid postId, [FromQuery] CommentPaginationParams @params)
        => HandlePagedList(await Mediator.Send(new GetPostCommentsWithPaginationQuery(postId, @params)));
}
