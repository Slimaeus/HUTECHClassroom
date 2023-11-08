using HUTECHClassroom.Application.Common.Requests;

namespace HUTECHClassroom.Application.Comments.Commands.CreateComment;

public sealed record CreateCommentCommand : CreateCommand
{
    public required string Content { get; set; }
    public Guid UserId { get; set; }
    public Guid PostId { get; set; }
}
public sealed class CreateCommentCommandHandler : CreateCommandHandler<Comment, CreateCommentCommand>
{
    public CreateCommentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}
