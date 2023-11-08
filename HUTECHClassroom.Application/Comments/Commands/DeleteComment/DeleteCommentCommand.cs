using HUTECHClassroom.Application.Comments.DTOs;
using HUTECHClassroom.Application.Common.Requests;

namespace HUTECHClassroom.Application.Comments.Commands.DeleteComment;

public record DeleteCommentCommand(Guid Id) : DeleteCommand<CommentDTO>(Id);
public sealed class DeleteCommentCommandHandler : DeleteCommandHandler<Comment, DeleteCommentCommand, CommentDTO>
{
    public DeleteCommentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}
