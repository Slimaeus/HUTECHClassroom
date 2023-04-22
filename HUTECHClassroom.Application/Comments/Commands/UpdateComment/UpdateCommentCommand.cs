using HUTECHClassroom.Application.Common.Requests;

namespace HUTECHClassroom.Application.Comments.Commands.UpdateComment;

public record UpdateCommentCommand(Guid Id) : UpdateCommand(Id)
{
    public string Content { get; set; }
}
public class UpdateCommentCommandHandler : UpdateCommandHandler<Comment, UpdateCommentCommand>
{
    public UpdateCommentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}
