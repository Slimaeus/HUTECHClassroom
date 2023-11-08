using HUTECHClassroom.Application.Common.Requests;

namespace HUTECHClassroom.Application.Comments.Commands.DeleteRangeComment;

public record DeleteRangeCommentCommand(IList<Guid> Ids) : DeleteRangeCommand(Ids);
public sealed class DeleteRangeCommentCommandHandler : DeleteRangeCommandHandler<Comment, DeleteRangeCommentCommand>
{
    public DeleteRangeCommentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}
