using HUTECHClassroom.Application.Comments;
using HUTECHClassroom.Application.Comments.Commands.CreateComment;
using HUTECHClassroom.Application.Comments.Commands.DeleteComment;
using HUTECHClassroom.Application.Comments.Commands.DeleteRangeComment;
using HUTECHClassroom.Application.Comments.Commands.UpdateComment;
using HUTECHClassroom.Application.Comments.DTOs;
using HUTECHClassroom.Application.Comments.Queries.GetComment;
using HUTECHClassroom.Application.Comments.Queries.GetCommentsWithPagination;
using Microsoft.AspNetCore.Mvc;

namespace HUTECHClassroom.API.Controllers.Api.V1;

[ApiVersion("1.0")]
public class CommentsController : BaseEntityApiController<CommentDTO>
{
    [HttpGet]
    public Task<ActionResult<IEnumerable<CommentDTO>>> Get([FromQuery] CommentPaginationParams @params)
        => HandlePaginationQuery<GetCommentsWithPaginationQuery, CommentPaginationParams>(new GetCommentsWithPaginationQuery(@params));
    [HttpGet("{id}", Name = nameof(GetCommentDetails))]
    public Task<ActionResult<CommentDTO>> GetCommentDetails(Guid id)
        => HandleGetQuery(new GetCommentQuery(id));
    [HttpPost]
    public Task<ActionResult<CommentDTO>> Comment(CreateCommentCommand command)
        => HandleCreateCommand(command, nameof(GetCommentDetails));
    [HttpPut("{id}")]
    public Task<IActionResult> Put(Guid id, UpdateCommentCommand request)
        => HandleUpdateCommand(id, request);
    [HttpDelete("{id}")]
    public Task<ActionResult<CommentDTO>> Delete(Guid id)
        => HandleDeleteCommand(new DeleteCommentCommand(id));
    [HttpDelete]
    public Task<IActionResult> DeleteRange(IList<Guid> ids)
        => HandleDeleteRangeCommand(new DeleteRangeCommentCommand(ids));
}
