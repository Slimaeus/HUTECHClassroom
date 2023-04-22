using EntityFrameworkCore.Repository.Extensions;
using HUTECHClassroom.Application.Common.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace HUTECHClassroom.Application.Classrooms.Commands.RemoveClassroomUser;

public record RemoveClassroomUserCommand(Guid Id, string UserName) : IRequest<Unit>;
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
            .AndFilter(x => x.Id == request.Id);

        var classroom = await _repository
            .SingleOrDefaultAsync(query, cancellationToken);

        if (classroom == null) throw new NotFoundException(nameof(Classroom), request.Id);

        var user = classroom.ClassroomUsers.SingleOrDefault(x => x.User.UserName == request.UserName);

        if (user == null) throw new NotFoundException(nameof(ApplicationUser), request.UserName);

        classroom.ClassroomUsers.Remove(user);

        await _unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

        _repository.RemoveTracking(classroom);

        return Unit.Value;
    }
}
