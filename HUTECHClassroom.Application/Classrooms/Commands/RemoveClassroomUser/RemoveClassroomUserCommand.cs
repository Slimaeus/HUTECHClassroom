using EntityFrameworkCore.Repository.Extensions;
using Microsoft.EntityFrameworkCore;

namespace HUTECHClassroom.Application.Classrooms.Commands.RemoveClassroomUser;

public record RemoveClassroomUserCommand(Guid ClassroomId, Guid UserId) : IRequest<Unit>;
public class RemoveClassroomUserCommandHandler : IRequestHandler<RemoveClassroomUserCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<Classroom> _repository;

    public RemoveClassroomUserCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _repository = unitOfWork.Repository<Classroom>();
    }
    public async Task<Unit> Handle(RemoveClassroomUserCommand request, CancellationToken cancellationToken)
    {
        var query = _repository
            .SingleResultQuery()
            .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll)
            .Include(i => i.Include(x => x.ClassroomUsers).ThenInclude(x => x.User))
            .AndFilter(x => x.Id == request.ClassroomId);

        var classroom = await _repository
            .SingleOrDefaultAsync(query, cancellationToken);

        var user = classroom.ClassroomUsers.SingleOrDefault(x => x.UserId == request.UserId);

        if (user == null) throw new InvalidOperationException($"User {request.UserId} did not join");

        classroom.ClassroomUsers.Remove(user);

        await _unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

        _repository.RemoveTracking(classroom);

        return Unit.Value;
    }
}
