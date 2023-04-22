using HUTECHClassroom.Application.Common.Exceptions;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Missions.DTOs;

namespace HUTECHClassroom.Application.Missions.Commands.CreateMission;

public record CreateMissionCommand : CreateCommand<MissionDTO>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsDone { get; set; } = false;
    public Guid ProjectId { get; set; }
}
public class CreateMissionCommandHandler : CreateCommandHandler<Mission, CreateMissionCommand, MissionDTO>
{
    private readonly IRepository<Project> _projectRepository;

    public CreateMissionCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
        _projectRepository = unitOfWork.Repository<Project>();
    }
    protected override async Task ValidateAdditionalField(CreateMissionCommand request, Mission entity)
    {
        var query = _projectRepository
            .SingleResultQuery()
            .AndFilter(x => x.Id == request.ProjectId);

        var project = await _projectRepository.SingleOrDefaultAsync(query);

        if (project == null) throw new NotFoundException(nameof(Project), request.ProjectId);

        entity.Project = project;
    }
}
