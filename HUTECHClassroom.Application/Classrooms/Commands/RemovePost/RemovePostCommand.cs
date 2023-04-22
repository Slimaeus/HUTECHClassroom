using EntityFrameworkCore.Repository.Extensions;
using HUTECHClassroom.Application.Common.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace HUTECHClassroom.Application.Classrooms.Commands.RemovePost;

public record RemovePostCommand(Guid Id, Guid PostId) : IRequest<Unit>;
public class RemovePostCommandHandler : IRequestHandler<RemovePostCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<Classroom> _repository;

    public RemovePostCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _repository = unitOfWork.Repository<Classroom>();
    }
    public async Task<Unit> Handle(RemovePostCommand request, CancellationToken cancellationToken)
    {
        var query = _repository
            .SingleResultQuery()
            .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll)
            .Include(i => i.Include(x => x.Posts).ThenInclude(x => x.User))
            .AndFilter(x => x.Id == request.Id);

        var classroom = await _repository
            .SingleOrDefaultAsync(query, cancellationToken);

        if (classroom == null) throw new NotFoundException(nameof(Classroom), request.Id);

        var user = classroom.Posts.SingleOrDefault(x => x.Id == request.PostId);

        if (user == null) throw new NotFoundException(nameof(ApplicationUser), request.PostId);

        classroom.Posts.Remove(user);

        await _unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

        _repository.RemoveTracking(classroom);

        return Unit.Value;
    }
}
