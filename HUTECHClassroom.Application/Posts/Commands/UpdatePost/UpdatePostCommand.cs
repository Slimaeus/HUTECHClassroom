using HUTECHClassroom.Application.Common.Requests;

namespace HUTECHClassroom.Application.Posts.Commands.UpdatePost;

public record UpdatePostCommand(Guid Id) : UpdateCommand(Id)
{
    public string Content { get; set; }
    public string Link { get; set; }
}
public class UpdatePostCommandHandler : UpdateCommandHandler<Post, UpdatePostCommand>
{
    public UpdatePostCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}
