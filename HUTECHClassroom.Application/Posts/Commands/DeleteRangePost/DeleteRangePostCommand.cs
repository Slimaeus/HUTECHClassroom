using HUTECHClassroom.Application.Common.Requests;

namespace HUTECHClassroom.Application.Posts.Commands.DeleteRangePost;

public record DeleteRangePostCommand(IList<Guid> Ids) : DeleteRangeCommand(Ids);
public class DeleteRangePostCommandHandler : DeleteRangeCommandHandler<Post, DeleteRangePostCommand>
{
    public DeleteRangePostCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}
