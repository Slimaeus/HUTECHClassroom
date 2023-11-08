using EntityFrameworkCore.Repository.Extensions;
using HUTECHClassroom.Application.Common.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace HUTECHClassroom.Application.Classrooms.Commands.AddPost;

public record AddPostCommand(Guid ClassroomId, Guid PostId) : IRequest<Unit>;
public sealed class AddPostCommandHandler : IRequestHandler<AddPostCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<Post> _postRepository;

    public AddPostCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _postRepository = _unitOfWork.Repository<Post>();
    }
    public async Task<Unit> Handle(AddPostCommand request, CancellationToken cancellationToken)
    {
        var postQuery = _postRepository
                .SingleResultQuery()
                .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll)
                .AndFilter(x => x.Id == request.PostId);

        var post = await _postRepository
            .SingleOrDefaultAsync(postQuery, cancellationToken);

        if (post is { } && post.ClassroomId == request.ClassroomId) throw new InvalidOperationException($"{request.PostId} already exists");

        if (post is null) throw new NotFoundException(typeof(Post).Name, request.PostId);

        post.ClassroomId = request.ClassroomId;

        await _unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

        _postRepository.RemoveTracking(post);

        return Unit.Value;
    }
}
