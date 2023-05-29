using EntityFrameworkCore.Repository.Extensions;
using HUTECHClassroom.Application.Common.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace HUTECHClassroom.Application.Classrooms.Commands.AddClassroomUser;

public record AddClassroomUserCommand(Guid Id, Guid UserId) : IRequest<Unit>;
public class AddClassroomUserCommandHandler : IRequestHandler<AddClassroomUserCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<Classroom> _repository;
    private readonly IRepository<ApplicationUser> _userRepository;

    public AddClassroomUserCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _repository = unitOfWork.Repository<Classroom>();
        _userRepository = _unitOfWork.Repository<ApplicationUser>();
    }
    public async Task<Unit> Handle(AddClassroomUserCommand request, CancellationToken cancellationToken)
    {
        var query = _repository
            .SingleResultQuery()
            .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll)
            .Include(i => i.Include(x => x.ClassroomUsers).ThenInclude(x => x.User))
            .AndFilter(x => x.Id == request.Id);

        var classroom = await _repository
            .SingleOrDefaultAsync(query, cancellationToken);

        if (classroom == null) throw new NotFoundException(nameof(Classroom), request.Id);

        if (classroom.ClassroomUsers.Any(x => x.UserId == request.UserId)) throw new InvalidOperationException($"User {request.UserId} already exists");

        if (request.UserId != Guid.Empty)
        {
            var userQuery = _userRepository
                .SingleResultQuery()
                .AndFilter(x => x.Id == request.UserId);

            var user = await _userRepository
                .SingleOrDefaultAsync(userQuery, cancellationToken);

            if (user != null)
            {
                classroom.ClassroomUsers.Add(new ClassroomUser { User = user });
            }
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

        _repository.RemoveTracking(classroom);

        return Unit.Value;
    }
}
