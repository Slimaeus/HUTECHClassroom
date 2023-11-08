using EntityFrameworkCore.Repository.Extensions;
using Microsoft.EntityFrameworkCore;

namespace HUTECHClassroom.Application.Classrooms.Commands.AddClassroomUser;

public record AddClassroomUserCommand(Guid ClassroomId, Guid UserId) : IRequest<Unit>;
public sealed class AddClassroomUserCommandHandler : IRequestHandler<AddClassroomUserCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IRepository<Classroom> _repository;
    private readonly IRepository<ApplicationUser> _userRepository;

    public AddClassroomUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _repository = unitOfWork.Repository<Classroom>();
        _userRepository = _unitOfWork.Repository<ApplicationUser>();
    }
    public async Task<Unit> Handle(AddClassroomUserCommand request, CancellationToken cancellationToken)
    {
        var query = _repository
            .SingleResultQuery()
            .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll)
            .Include(i => i.Include(x => x.ClassroomUsers))
            .AndFilter(x => x.Id == request.ClassroomId);

        var classroom = await _repository
            .SingleOrDefaultAsync(query, cancellationToken);

        if (classroom.ClassroomUsers.Any(x => x.UserId == request.UserId)) throw new InvalidOperationException($"User {request.UserId} already exists");

        var classroomUser = _mapper.Map<ClassroomUser>(request);

        classroom.ClassroomUsers.Add(classroomUser);

        await _unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

        _repository.RemoveTracking(classroom);

        return Unit.Value;
    }
}
