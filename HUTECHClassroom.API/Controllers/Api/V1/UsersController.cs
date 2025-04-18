﻿using HUTECHClassroom.Application.Classes.DTOs;
using HUTECHClassroom.Application.Users.Queries.GetUserClassesWithPagination;
using HUTECHClassroom.Application.Users.Queries.GetUsersByIds;
using HUTECHClassroom.Application.Users.Queries.GetUsersWithPagination;

namespace HUTECHClassroom.API.Controllers.Api.V1;

[ApiVersion("1.0")]
[Authorize]
public sealed class UsersController : BaseEntityApiController<UserDTO>
{
    [AllowAnonymous]
    [HttpGet("{userId}")]
    public Task<ActionResult<UserDTO>> GetUserDetails(Guid userId)
        => HandleGetQuery(new GetUserQuery(userId));

    [AllowAnonymous]
    [HttpGet("ids")]
    public Task<ActionResult<IEnumerable<UserDTO>>> GetRange([FromQuery] IList<Guid> userIds, [FromQuery] PaginationParams @params)
        => HandlePaginationQuery<GetUsersByIdsQuery, PaginationParams>(new GetUsersByIdsQuery(userIds, @params));

    [AllowAnonymous]
    [HttpGet("@me/users")]
    public Task<ActionResult<IEnumerable<UserDTO>>> GetUsers([FromQuery] UserPaginationParams @params)
        => HandlePaginationQuery<GetUsersWithPaginationQuery, UserPaginationParams>(new GetUsersWithPaginationQuery(@params));

    [HttpGet("@me")]
    public async Task<ActionResult<AccountDTO>> GetCurrentUserDetails()
        => Ok(await Mediator.Send(new GetCurrentUserQuery()));

    [HttpGet("@me/classes")]
    public async Task<ActionResult<IEnumerable<ClassDTO>>> GetClasses([FromQuery] PaginationParams @params)
        => HandlePagedList(await Mediator.Send(new GetUserClassesWithPaginationQuery(@params)));
    [HttpGet("@me/classrooms")]
    public async Task<ActionResult<IEnumerable<ClassroomDTO>>> GetClassrooms([FromQuery] PaginationParams @params)
        => HandlePagedList(await Mediator.Send(new GetUserClassroomsWithPaginationQuery(@params)));

    [HttpGet("@me/groups")]
    public async Task<ActionResult<IEnumerable<GroupDTO>>> GetGroups([FromQuery] PaginationParams @params)
        => HandlePagedList(await Mediator.Send(new GetUserGroupsWithPaginationQuery(@params)));

    [HttpGet("@me/projects")]
    public async Task<ActionResult<IEnumerable<ProjectDTO>>> GetProjects([FromQuery] PaginationParams @params)
        => HandlePagedList(await Mediator.Send(new GetUserProjectsWithPaginationQuery(@params)));

    [HttpGet("@me/missions")]
    public async Task<ActionResult<IEnumerable<MissionDTO>>> GetMissions([FromQuery] PaginationParams @params)
        => HandlePagedList(await Mediator.Send(new GetUserMissionsWithPaginationQuery(@params)));

    [HttpGet("@me/exercises")]
    public async Task<ActionResult<IEnumerable<ExerciseDTO>>> GetExercises([FromQuery] PaginationParams @params)
        => HandlePagedList(await Mediator.Send(new GetUserExercisesWithPaginationQuery(@params)));

    [HttpGet("@me/answers")]
    public async Task<ActionResult<IEnumerable<AnswerDTO>>> GetAnswers([FromQuery] PaginationParams @params)
        => HandlePagedList(await Mediator.Send(new GetUserAnswersWithPaginationQuery(@params)));

    [HttpGet("@me/posts")]
    public async Task<ActionResult<IEnumerable<PostDTO>>> GetPosts([FromQuery] PaginationParams @params)
        => HandlePagedList(await Mediator.Send(new GetUserPostsWithPaginationQuery(@params)));

    [HttpGet("@me/comments")]
    public async Task<ActionResult<IEnumerable<CommentDTO>>> GetComments([FromQuery] PaginationParams @params)
        => HandlePagedList(await Mediator.Send(new GetUserCommentsWithPaginationQuery(@params)));

    [HttpGet("@me/roles")]
    public async Task<ActionResult<IEnumerable<RoleDTO>>> GetRoles([FromQuery] PaginationParams @params)
        => HandlePagedList(await Mediator.Send(new GetUserRolesWithPaginationQuery(@params)));
}
