using HUTECHClassroom.Application.Common.DTOs;
using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Groups.Commands.AddGroupUser;
using HUTECHClassroom.Application.Groups.Commands.CreateGroup;
using HUTECHClassroom.Application.Groups.Commands.DeleteGroup;
using HUTECHClassroom.Application.Groups.Commands.DeleteRangeGroup;
using HUTECHClassroom.Application.Groups.Commands.RemoveGroupUser;
using HUTECHClassroom.Application.Groups.Commands.UpdateGroup;
using HUTECHClassroom.Application.Groups.DTOs;
using HUTECHClassroom.Application.Groups.Queries.GetGroup;
using HUTECHClassroom.Application.Groups.Queries.GetGroupProjectsWithPagination;
using HUTECHClassroom.Application.Groups.Queries.GetGroupsWithPagination;
using HUTECHClassroom.Application.Groups.Queries.GetGroupUser;
using HUTECHClassroom.Application.Groups.Queries.GetGroupUsersWithPagination;
using Microsoft.AspNetCore.Mvc;

namespace HUTECHClassroom.API.Controllers.Api.V1;

[ApiVersion("1.0")]
public class GroupsController : BaseEntityApiController<GroupDTO>
{
    [HttpGet]
    public Task<ActionResult<IEnumerable<GroupDTO>>> Get([FromQuery] PaginationParams @params)
        => HandlePaginationQuery<GetGroupsWithPaginationQuery, PaginationParams>(new GetGroupsWithPaginationQuery(@params));
    [HttpGet("{id}", Name = nameof(GetGroupDetails))]
    public Task<ActionResult<GroupDTO>> GetGroupDetails(Guid id)
        => HandleGetQuery(new GetGroupQuery(id));
    [HttpPost]
    public Task<ActionResult<GroupDTO>> Post(CreateGroupCommand command)
        => HandleCreateCommand(command, nameof(GetGroupDetails));
    [HttpPut("{id}")]
    public Task<IActionResult> Put(Guid id, UpdateGroupCommand request)
        => HandleUpdateCommand(id, request);
    [HttpDelete("{id}")]
    public Task<ActionResult<GroupDTO>> Delete(Guid id)
        => HandleDeleteCommand(new DeleteGroupCommand(id));
    [HttpDelete]
    public Task<IActionResult> DeleteRange(IList<Guid> ids)
        => HandleDeleteRangeCommand(new DeleteRangeGroupCommand(ids));
    [HttpGet("{id}/members")]
    public async Task<ActionResult<IEnumerable<MemberDTO>>> GetMembers(Guid id, [FromQuery] PaginationParams @params)
        => HandlePagedList(await Mediator.Send(new GetGroupUsersWithPaginationQuery(id, @params)));
    // Same as Get User Profile
    [HttpGet("{id}/members/{userName}")]
    public async Task<ActionResult<MemberDTO>> GetMember(Guid id, string userName)
        => Ok(await Mediator.Send(new GetGroupUserQuery(id, userName)));
    [HttpPost("{id}/members/{userName}")]
    public async Task<IActionResult> AddMember(Guid id, string userName)
        => Ok(await Mediator.Send(new AddGroupUserCommand(id, userName)));
    [HttpDelete("{id}/members/{userName}")]
    public async Task<IActionResult> RemoveMember(Guid id, string userName)
        => Ok(await Mediator.Send(new RemoveGroupUserCommand(id, userName)));
    [HttpGet("{id}/projects")]
    public async Task<ActionResult<IEnumerable<GroupProjectDTO>>> GetProjects(Guid id, [FromQuery] PaginationParams @params)
        => HandlePagedList(await Mediator.Send(new GetGroupProjectsWithPaginationQuery(id, @params)));
}
