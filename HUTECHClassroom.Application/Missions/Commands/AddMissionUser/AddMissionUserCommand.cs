using EntityFrameworkCore.Repository.Extensions;
using Microsoft.EntityFrameworkCore;

namespace HUTECHClassroom.Application.Missions.Commands.AddMissionUser;

public record AddMissionUserCommand(Guid MissionId, Guid UserId) : IRequest<Unit>;
public sealed class AddMissionUserCommandHandler : IRequestHandler<AddMissionUserCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IRepository<Mission> _repository;
    private readonly IRepository<ApplicationUser> _userRepository;

    public AddMissionUserCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _repository = unitOfWork.Repository<Mission>();
        _userRepository = unitOfWork.Repository<ApplicationUser>();
    }
    public async Task<Unit> Handle(AddMissionUserCommand request, CancellationToken cancellationToken)
    {
        var query = _repository
            .SingleResultQuery()
            .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll)
            .Include(i => i.Include(x => x.MissionUsers).ThenInclude(x => x.User))
            .AndFilter(x => x.Id == request.MissionId);

        var mission = await _repository
            .SingleOrDefaultAsync(query, cancellationToken);

        if (mission.MissionUsers.Any(x => x.UserId == request.UserId))
            throw new InvalidOperationException($"{request.UserId} already exists");

        var missionUser = _mapper.Map<MissionUser>(request);

        mission.MissionUsers.Add(missionUser);

        await _unitOfWork
            .SaveChangesAsync(cancellationToken: cancellationToken)
            .ConfigureAwait(false);

        _repository.RemoveTracking(mission);

        return Unit.Value;
    }
}
