using HUTECHClassroom.Application.Comments;
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
    [HttpGet("{id}", Name = nameof(GetPostDetails))]
    public Task<ActionResult<PostDTO>> GetPostDetails(Guid id)
        => HandleGetQuery(new GetPostQuery(id));
    [HttpPost]
    public Task<ActionResult<PostDTO>> Post(CreatePostCommand command)
        => HandleCreateCommand(command, nameof(GetPostDetails));
    [HttpPut("{id}")]
    public Task<IActionResult> Put(Guid id, UpdatePostCommand request)
        => HandleUpdateCommand(id, request);
    [HttpDelete("{id}")]
    public Task<ActionResult<PostDTO>> Delete(Guid id)
        => HandleDeleteCommand(new DeletePostCommand(id));
    [HttpDelete]
    public Task<IActionResult> DeleteRange(IList<Guid> ids)
        => HandleDeleteRangeCommand(new DeleteRangePostCommand(ids));
    [HttpGet("{id}/comments")]
    public async Task<ActionResult<IEnumerable<PostCommentDTO>>> GetComments(Guid id, [FromQuery] CommentPaginationParams @params)
        => HandlePagedList(await Mediator.Send(new GetPostCommentsWithPaginationQuery(id, @params)));
}
