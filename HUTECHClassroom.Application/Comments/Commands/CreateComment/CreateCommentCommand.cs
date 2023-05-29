using HUTECHClassroom.Application.Common.Requests;

namespace HUTECHClassroom.Application.Comments.Commands.CreateComment;

public record CreateCommentCommand : CreateCommand
{
    public string Content { get; set; }
    public Guid UserId { get; set; }
    public Guid PostId { get; set; }
}
public class CreateCommentCommandHandler : CreateCommandHandler<Comment, CreateCommentCommand>
{
    public CreateCommentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}
