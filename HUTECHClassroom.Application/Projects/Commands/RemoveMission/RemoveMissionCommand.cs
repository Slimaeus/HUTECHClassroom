using EntityFrameworkCore.Repository.Extensions;
using EntityFrameworkCore.Repository.Interfaces;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using HUTECHClassroom.Application.Common.Exceptions;
using HUTECHClassroom.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HUTECHClassroom.Application.Projects.Commands.RemoveMission;

public record RemoveMissionCommand(Guid Id, Guid MissionId) : IRequest<Unit>;
public class RemoveMissionCommandHandler : IRequestHandler<RemoveMissionCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<Project> _repository;

    public RemoveMissionCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _repository = unitOfWork.Repository<Project>();
    }
    public async Task<Unit> Handle(RemoveMissionCommand request, CancellationToken cancellationToken)
    {
        var query = _repository
            .SingleResultQuery()
            .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll)
            .Include(i => i.Include(x => x.Missions))
            .AndFilter(x => x.Id == request.Id);

        var project = await _repository
            .SingleOrDefaultAsync(query, cancellationToken);

        if (project == null) throw new NotFoundException(nameof(Mission), request.Id);

        var mission = project.Missions.SingleOrDefault(x => x.Id == request.MissionId);

        if (mission == null) throw new NotFoundException(nameof(Mission), request.MissionId);

        project.Missions.Remove(mission);

        await _unitOfWork.SaveChangesAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

        _repository.RemoveTracking(project);

        return Unit.Value;
    }
}
