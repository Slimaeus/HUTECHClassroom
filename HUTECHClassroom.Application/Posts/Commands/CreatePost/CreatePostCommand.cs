using HUTECHClassroom.Application.Common.Requests;

namespace HUTECHClassroom.Application.Posts.Commands.CreatePost;

public record CreatePostCommand : CreateCommand
{
    public string Content { get; set; }
    public string Link { get; set; }
    public Guid UserId { get; set; }
    public Guid ClassroomId { get; set; }
}
public class CreatePostCommandHandler : CreateCommandHandler<Post, CreatePostCommand>
{
    public CreatePostCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}
