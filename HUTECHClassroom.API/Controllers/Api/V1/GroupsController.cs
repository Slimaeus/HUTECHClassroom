using HUTECHClassroom.Application.Common.DTOs;
using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Groups;
using HUTECHClassroom.Application.Groups.Commands.AddGroupLeader;
using HUTECHClassroom.Application.Groups.Commands.AddGroupUser;
using HUTECHClassroom.Application.Groups.Commands.CreateGroup;
using HUTECHClassroom.Application.Groups.Commands.DeleteGroup;
using HUTECHClassroom.Application.Groups.Commands.DeleteRangeGroup;
using HUTECHClassroom.Application.Groups.Commands.RemoveGroupLeader;
using HUTECHClassroom.Application.Groups.Commands.RemoveGroupUser;
using HUTECHClassroom.Application.Groups.Commands.UpdateGroup;
using HUTECHClassroom.Application.Groups.DTOs;
using HUTECHClassroom.Application.Groups.Queries.GetGroup;
using HUTECHClassroom.Application.Groups.Queries.GetGroupProjectsWithPagination;
using HUTECHClassroom.Application.Groups.Queries.GetGroupsWithPagination;
using HUTECHClassroom.Application.Groups.Queries.GetGroupUser;
using HUTECHClassroom.Application.Groups.Queries.GetGroupUsersWithPagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HUTECHClassroom.API.Controllers.Api.V1;

[ApiVersion("1.0")]
public class GroupsController : BaseEntityApiController<GroupDTO>
{
    [HttpGet]
    public Task<ActionResult<IEnumerable<GroupDTO>>> Get([FromQuery] GroupPaginationParams @params)
        => HandlePaginationQuery<GetGroupsWithPaginationQuery, GroupPaginationParams>(new GetGroupsWithPaginationQuery(@params));
    [Authorize(Policy = RequireLeaderGroupRolePolicy)]
    [HttpGet("{groupId}", Name = nameof(GetGroupDetails))]
    public Task<ActionResult<GroupDTO>> GetGroupDetails(Guid groupId)
        => HandleGetQuery(new GetGroupQuery(groupId));
    [HttpPost]
    public Task<ActionResult<GroupDTO>> Post(CreateGroupCommand command)
        => HandleCreateCommand(command, nameof(GetGroupDetails));
    [Authorize(Policy = RequireLeaderGroupRolePolicy)]
    [HttpPut("{groupId}")]
    public Task<IActionResult> Put(Guid groupId, UpdateGroupCommand request)
        => HandleUpdateCommand(groupId, request);
    [Authorize(Policy = RequireLeaderGroupRolePolicy)]
    [HttpDelete("{groupId}")]
    public Task<ActionResult<GroupDTO>> Delete(Guid groupId)
        => HandleDeleteCommand(new DeleteGroupCommand(groupId));
    [HttpDelete]
    public Task<IActionResult> DeleteRange(IList<Guid> groupIds)
        => HandleDeleteRangeCommand(new DeleteRangeGroupCommand(groupIds));
    [HttpGet("{groupId}/members")]
    public async Task<ActionResult<IEnumerable<GroupUserDTO>>> GetMembers(Guid groupId, [FromQuery] PaginationParams @params)
        => HandlePagedList(await Mediator.Send(new GetGroupUsersWithPaginationQuery(groupId, @params)));
    // Same as Get User Profile
    [HttpGet("{groupId}/members/{userName}")]
    public async Task<ActionResult<MemberDTO>> GetMember(Guid groupId, string userName)
        => Ok(await Mediator.Send(new GetGroupUserQuery(groupId, userName)));
    [HttpPost("{groupId}/members/{userName}")]
    public async Task<IActionResult> AddMember(Guid groupId, string userName)
        => Ok(await Mediator.Send(new AddGroupUserCommand(groupId, userName)));
    [HttpDelete("{groupId}/members/{userName}")]
    public async Task<IActionResult> RemoveMember(Guid groupId, string userName)
        => Ok(await Mediator.Send(new RemoveGroupUserCommand(groupId, userName)));
    [HttpGet("{groupId}/projects")]
    public async Task<ActionResult<IEnumerable<GroupProjectDTO>>> GetProjects(Guid groupId, [FromQuery] PaginationParams @params)
        => HandlePagedList(await Mediator.Send(new GetGroupProjectsWithPaginationQuery(groupId, @params)));
    [HttpPost("{groupId}/add-leader/{userName}")]
    public async Task<IActionResult> AddLeader(Guid groupId, string userName)
    {
        await Mediator.Send(new AddGroupLeaderCommand(groupId, userName));
        return NoContent();
    }
    [HttpDelete("{groupId}/remove-leader/{userName}")]
    public async Task<IActionResult> RemoveLeader(Guid groupId, string userName)
    {
        await Mediator.Send(new RemoveGroupLeaderCommand(groupId, userName));
        return NoContent();
    }
}