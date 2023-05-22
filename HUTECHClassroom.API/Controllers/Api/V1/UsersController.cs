using HUTECHClassroom.Application.Answers.DTOs;
using HUTECHClassroom.Application.Classrooms.DTOs;
using HUTECHClassroom.Application.Comments.DTOs;
using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Exercises.DTOs;
using HUTECHClassroom.Application.Groups.DTOs;
using HUTECHClassroom.Application.Missions.DTOs;
using HUTECHClassroom.Application.Posts.DTOs;
using HUTECHClassroom.Application.Projects.DTOs;
using HUTECHClassroom.Application.Roles.DTOs;
using HUTECHClassroom.Application.Users.DTOs;
using HUTECHClassroom.Application.Users.Queries.GetUser;
using HUTECHClassroom.Application.Users.Queries.GetUserAnswersWithPagination;
using HUTECHClassroom.Application.Users.Queries.GetUserClassroomsWithPagination;
using HUTECHClassroom.Application.Users.Queries.GetUserCommentsWithPagination;
using HUTECHClassroom.Application.Users.Queries.GetUserExercisesWithPagination;
using HUTECHClassroom.Application.Users.Queries.GetUserGroupsWithPagination;
using HUTECHClassroom.Application.Users.Queries.GetUserMissionsWithPagination;
using HUTECHClassroom.Application.Users.Queries.GetUserPostsWithPagination;
using HUTECHClassroom.Application.Users.Queries.GetUserProjectsWithPagination;
using HUTECHClassroom.Application.Users.Queries.GetUserRolesWithPagination;
using HUTECHClassroom.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Immutable;

namespace HUTECHClassroom.API.Controllers.Api.V1;

[ApiVersion("1.0")]
[Authorize]
public class UsersController : BaseEntityApiController<UserDTO>
{
    private readonly IUserAccessor _userAccessor;

    public UsersController(IUserAccessor userAccessor)
    {
        _userAccessor = userAccessor;
    }
    [HttpGet("get-roles")]
    public ActionResult<IList<string>> GetRoles()
    {
        return Ok(_userAccessor.Roles);
    }
    [HttpGet("get-entity-claims")]
    public ActionResult<IDictionary<string, ImmutableArray<string>>> GetEntityClaims()
    {
        return Ok(_userAccessor.EntityClaims);
    }
    [AllowAnonymous]
    [HttpGet("{userName}")]
    public Task<ActionResult<UserDTO>> GetUserDetails(string userName)
        => HandleGetQuery(new GetUserQuery(userName));
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
