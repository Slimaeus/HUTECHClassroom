using EntityFrameworkCore.Repository.Extensions;
using EntityFrameworkCore.Repository.Interfaces;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using HUTECHClassroom.Application.Common.Exceptions;
using HUTECHClassroom.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HUTECHClassroom.Application.Classrooms.Commands.AddPost;

public record AddPostCommand(Guid Id, Guid PostId) : IRequest<Unit>;
public class AddPostCommandHandler : IRequestHandler<AddPostCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<Classroom> _repository;
    private readonly IRepository<Post> _postRepository;

    public AddPostCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _repository = unitOfWork.Repository<Classroom>();
        _postRepository = _unitOfWork.Repository<Post>();
    }
    public async Task<Unit> Handle(AddPostCommand request, CancellationToken cancellationToken)
    {
        var query = _repository
            .SingleResultQuery()
            .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll)
            .Include(i => i.Include(x => x.Posts).ThenInclude(x => x.User))
            .AndFilter(x => x.Id == request.Id);

        var classroom = await _repository
            .SingleOrDefaultAsync(query, cancellationToken);

        if (classroom == null) throw new NotFoundException(nameof(Classroom), request.Id);

        if (classroom.Posts.Any(x => x.Id == request.PostId)) throw new InvalidOperationException($"{request.PostId} already exists");

        var postQuery = _postRepository
            .SingleResultQuery()
            .AndFilter(x => x.Id == request.PostId);

        var post = await _postRepository
            .SingleOrDefaultAsync(postQuery, cancellationToken);

        if (post == null) throw new NotFoundException(nameof(Post), request.PostId);

        classroom.Posts.Add(post);

        await _unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

        _repository.RemoveTracking(classroom);

        return Unit.Value;
    }
}
