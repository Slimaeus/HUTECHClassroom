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
    [HttpGet("{commentId}")]
    public Task<ActionResult<CommentDTO>> GetCommentDetails(Guid commentId)
        => HandleGetQuery(new GetCommentQuery(commentId));
    [HttpPost]
    public Task<ActionResult<CommentDTO>> Comment(CreateCommentCommand command)
        => HandleCreateCommand(command, commentId => new GetCommentQuery(commentId));
    [HttpPut("{commentId}")]
    public Task<IActionResult> Put(Guid commentId, UpdateCommentCommand request)
        => HandleUpdateCommand(commentId, request);
    [HttpDelete("{commentId}")]
    public Task<ActionResult<CommentDTO>> Delete(Guid commentId)
        => HandleDeleteCommand(new DeleteCommentCommand(commentId));
    [HttpDelete]
    public Task<IActionResult> DeleteRange(IList<Guid> commentIds)
        => HandleDeleteRangeCommand(new DeleteRangeCommentCommand(commentIds));
}
