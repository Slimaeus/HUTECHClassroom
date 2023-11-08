using EntityFrameworkCore.Repository.Extensions;
using HUTECHClassroom.Application.Common.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace HUTECHClassroom.Application.Projects.Commands.AddMission;

public record AddMissionCommand(Guid Id, Guid MissionId) : IRequest<Unit>;
public sealed class AddMissionCommandHandler : IRequestHandler<AddMissionCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<Project> _repository;
    private readonly IRepository<Mission> _missionRepository;

    public AddMissionCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _repository = unitOfWork.Repository<Project>();
        _missionRepository = unitOfWork.Repository<Mission>();
    }
    public async Task<Unit> Handle(AddMissionCommand request, CancellationToken cancellationToken)
    {
        var query = _repository
            .SingleResultQuery()
            .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll)
            .Include(i => i.Include(x => x.Missions))
            .AndFilter(x => x.Id == request.Id);

        var project = await _repository
            .SingleOrDefaultAsync(query, cancellationToken);

        if (project == null) throw new NotFoundException(nameof(Mission), request.Id);

        if (project.Missions.Any(x => x.Id == request.MissionId)) throw new InvalidOperationException($"{request.MissionId} already exists");

        var missionQuery = _missionRepository
            .SingleResultQuery()
            .AndFilter(x => x.Id == request.MissionId);

        var mission = await _missionRepository.SingleOrDefaultAsync(missionQuery, cancellationToken);

        if (mission == null) throw new NotFoundException(nameof(Mission), request.MissionId);

        project.Missions.Add(mission);

        await _unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

        _repository.RemoveTracking(project);

        return Unit.Value;
    }
}
