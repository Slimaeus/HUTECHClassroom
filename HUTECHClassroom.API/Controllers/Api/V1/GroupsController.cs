namespace HUTECHClassroom.API.Controllers.Api.V1;

[ApiVersion("1.0")]
public sealed class GroupsController : BaseEntityApiController<GroupDTO>
{
    [Authorize(Policy = ReadGroupPolicy)]
    [HttpGet]
    public Task<ActionResult<IEnumerable<GroupDTO>>> Get([FromQuery] GroupPaginationParams @params)
        => HandlePaginationQuery<GetGroupsWithPaginationQuery, GroupPaginationParams>(new GetGroupsWithPaginationQuery(@params));

    [Authorize(Policy = ReadGroupPolicy)]
    [HttpGet("{groupId}")]
    public Task<ActionResult<GroupDTO>> GetGroupDetails(Guid groupId)
        => HandleGetQuery(new GetGroupQuery(groupId));

    [Authorize(Policy = CreateGroupPolicy)]
    [HttpPost]
    public Task<ActionResult<GroupDTO>> Post(CreateGroupCommand command)
        => HandleCreateCommand<CreateGroupCommand, GetGroupQuery>(command);

    [Authorize(Policy = UpdateGroupPolicy)]
    [HttpPut("{groupId}")]
    public Task<IActionResult> Put(Guid groupId, UpdateGroupCommand request)
        => HandleUpdateCommand(groupId, request);

    [Authorize(Policy = DeleteGroupPolicy)]
    [HttpDelete("{groupId}")]
    public Task<ActionResult<GroupDTO>> Delete(Guid groupId)
        => HandleDeleteCommand(new DeleteGroupCommand(groupId));

    [Authorize(Policy = DeleteGroupPolicy)]
    [HttpDelete]
    public Task<IActionResult> DeleteRange(IList<Guid> groupIds)
        => HandleDeleteRangeCommand(new DeleteRangeGroupCommand(groupIds));

    [Authorize(Policy = ReadGroupPolicy)]
    [HttpGet("{groupId}/members")]
    public async Task<ActionResult<IEnumerable<GroupUserDTO>>> GetMembers(Guid groupId, [FromQuery] PaginationParams @params)
        => HandlePagedList(await Mediator.Send(new GetGroupUsersWithPaginationQuery(groupId, @params)));

    [Authorize(Policy = AddGroupUserPolicy)]
    [HttpPost("{groupId}/members/{userId}")]
    public async Task<IActionResult> AddMember(Guid groupId, Guid userId)
        => Ok(await Mediator.Send(new AddGroupUserCommand(groupId, userId)));

    [Authorize(Policy = RemoveGroupUserPolicy)]
    [HttpDelete("{groupId}/members/{userId}")]
    public async Task<IActionResult> RemoveMember(Guid groupId, Guid userId)
        => Ok(await Mediator.Send(new RemoveGroupUserCommand(groupId, userId)));

    [Authorize(Policy = AddGroupUserPolicy)]
    [HttpPost("{groupId}/members/add")]
    public async Task<IActionResult> AddMembers(Guid groupId, IList<Guid> userIds)
        => Ok(await Mediator.Send(new AddRangeGroupUserCommand(groupId, userIds)));

    [Authorize(Policy = RemoveGroupUserPolicy)]
    [HttpPost("{groupId}/members/remove")]
    public async Task<IActionResult> RemoveMembers(Guid groupId, IList<Guid> userIds)
        => Ok(await Mediator.Send(new RemoveRangeGroupUserCommand(groupId, userIds)));

    [Authorize(Policy = ReadGroupPolicy)]
    [HttpGet("{groupId}/projects")]
    public async Task<ActionResult<IEnumerable<ProjectDTO>>> GetProjects(Guid groupId, [FromQuery] GroupPaginationParams @params)
        => HandlePagedList(await Mediator.Send(new GetGroupProjectsWithPaginationQuery(groupId, @params)));

    [Authorize(Policy = AddGroupLeaderPolicy)]
    [HttpPost("{groupId}/add-leader/{userId}")]
    public async Task<IActionResult> AddLeader(Guid groupId, Guid userId)
    {
        await Mediator.Send(new AddGroupLeaderCommand(groupId, userId));
        return NoContent();
    }

    [Authorize(Policy = RemoveGroupLeaderPolicy)]
    [HttpDelete("{groupId}/remove-leader/{userId}")]
    public async Task<IActionResult> RemoveLeader(Guid groupId, Guid userId)
    {
        await Mediator.Send(new RemoveGroupLeaderCommand(groupId, userId));
        return NoContent();
    }
}