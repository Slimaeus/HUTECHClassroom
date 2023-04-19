using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Posts.Commands.CreatePost;
using HUTECHClassroom.Application.Posts.Commands.DeletePost;
using HUTECHClassroom.Application.Posts.Commands.UpdatePost;
using HUTECHClassroom.Application.Posts.DTOs;
using HUTECHClassroom.Application.Posts.Queries.GetPost;
using HUTECHClassroom.Application.Posts.Queries.GetPostsWithPagination;
using Microsoft.AspNetCore.Mvc;

namespace HUTECHClassroom.API.Controllers.Api.V1;

[ApiVersion("1.0")]
public class PostsController : BaseEntityApiController<PostDTO>
{
    [HttpGet]
    public Task<ActionResult<IEnumerable<PostDTO>>> Get([FromQuery] PaginationParams @params)
        => HandlePaginationQuery(new GetPostsWithPaginationQuery(@params));
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
}
