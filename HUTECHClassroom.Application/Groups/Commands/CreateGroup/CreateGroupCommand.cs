using HUTECHClassroom.Application.Common.Exceptions;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Groups.DTOs;

namespace HUTECHClassroom.Application.Groups.Commands.CreateGroup;

public record CreateGroupCommand : CreateCommand<GroupDTO>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string LeaderName { get; set; }
    public Guid ClassroomId { get; set; }
}
public class CreateGroupCommandHandler : CreateCommandHandler<Group, CreateGroupCommand, GroupDTO>
{
    private readonly IRepository<ApplicationUser> _userRepository;
    private readonly IRepository<Classroom> _classroomRepository;

    public CreateGroupCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
        _userRepository = unitOfWork.Repository<ApplicationUser>();
        _classroomRepository = unitOfWork.Repository<Classroom>();
    }
    protected override async Task ValidateAdditionalField(CreateGroupCommand request, Group entity)
    {
        var userQuery = _userRepository
            .SingleResultQuery()
            .AndFilter(x => x.UserName == request.LeaderName);

        var leader = await _userRepository.SingleOrDefaultAsync(userQuery);

        if (leader == null) throw new NotFoundException(nameof(ApplicationUser), request.LeaderName);

        entity.Leader = leader;

        var classroomQuery = _classroomRepository
            .SingleResultQuery()
            .AndFilter(x => x.Id == request.ClassroomId);

        var classroom = await _classroomRepository.SingleOrDefaultAsync(classroomQuery);

        if (classroom == null) throw new NotFoundException(nameof(Classroom), request.ClassroomId);

        entity.Classroom = classroom;
    }
}
