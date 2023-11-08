using HUTECHClassroom.Application.Common.Requests;

namespace HUTECHClassroom.Application.Comments.Commands.UpdateComment;

public sealed record UpdateCommentCommand(Guid Id) : UpdateCommand(Id)
{
    public string? Content { get; set; }
}
public sealed class UpdateCommentCommandHandler : UpdateCommandHandler<Comment, UpdateCommentCommand>
{
    public UpdateCommentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}
