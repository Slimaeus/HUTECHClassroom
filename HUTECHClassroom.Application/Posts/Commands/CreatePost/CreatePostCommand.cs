using HUTECHClassroom.Application.Common.Exceptions;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Posts.DTOs;

namespace HUTECHClassroom.Application.Posts.Commands.CreatePost;

public record CreatePostCommand : CreateCommand<PostDTO>
{
    public string Content { get; set; }
    public string Link { get; set; }
    public string UserName { get; set; }
    public Guid ClassroomId { get; set; }
}
public class CreatePostCommandHandler : CreateCommandHandler<Post, CreatePostCommand, PostDTO>
{
    private readonly IRepository<ApplicationUser> _userRepository;
    private readonly IRepository<Classroom> _classroomRepository;
    public CreatePostCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
        _userRepository = unitOfWork.Repository<ApplicationUser>();
        _classroomRepository = unitOfWork.Repository<Classroom>();
    }
    protected override async Task ValidateAdditionalField(CreatePostCommand request, Post entity)
    {
        var userQuery = _userRepository
            .SingleResultQuery()
            .AndFilter(x => x.UserName == request.UserName);

        var user = await _userRepository.SingleOrDefaultAsync(userQuery);

        if (user == null) throw new NotFoundException(nameof(ApplicationUser), request.UserName);

        entity.User = user;

        var classroomQuery = _classroomRepository
            .SingleResultQuery()
            .AndFilter(x => x.Id == request.ClassroomId);

        var classroom = await _classroomRepository.SingleOrDefaultAsync(classroomQuery);

        if (classroom == null) throw new NotFoundException(nameof(Classroom), request.ClassroomId);

        entity.Classroom = classroom;
    }
}
