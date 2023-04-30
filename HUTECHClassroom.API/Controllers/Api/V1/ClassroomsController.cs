﻿using HUTECHClassroom.Application.Classrooms.Commands.AddClassroomUser;
using HUTECHClassroom.Application.Classrooms.Commands.CreateClassroom;
using HUTECHClassroom.Application.Classrooms.Commands.DeleteClassroom;
using HUTECHClassroom.Application.Classrooms.Commands.DeleteRangeClassroom;
using HUTECHClassroom.Application.Classrooms.Commands.RemoveClassroomUser;
using HUTECHClassroom.Application.Classrooms.Commands.UpdateClassroom;
using HUTECHClassroom.Application.Classrooms.DTOs;
using HUTECHClassroom.Application.Classrooms.Queries.GetClassroom;
using HUTECHClassroom.Application.Classrooms.Queries.GetClassroomExercisesWithPagination;
using HUTECHClassroom.Application.Classrooms.Queries.GetClassroomGroupsWithPagination;
using HUTECHClassroom.Application.Classrooms.Queries.GetClassroomPostsWithPagination;
using HUTECHClassroom.Application.Classrooms.Queries.GetClassroomsWithPagination;
using HUTECHClassroom.Application.Classrooms.Queries.GetClassroomUsersWithPagination;
using HUTECHClassroom.Application.Common.DTOs;
using HUTECHClassroom.Application.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace HUTECHClassroom.API.Controllers.Api.V1;

[ApiVersion("1.0")]
public class ClassroomsController : BaseEntityApiController<ClassroomDTO>
{
    [HttpGet]
    public Task<ActionResult<IEnumerable<ClassroomDTO>>> Get([FromQuery] PaginationParams @params)
        => HandlePaginationQuery<GetClassroomsWithPaginationQuery, PaginationParams>(new GetClassroomsWithPaginationQuery(@params));
    [HttpGet("{id}", Name = nameof(GetClassroomDetails))]
    public Task<ActionResult<ClassroomDTO>> GetClassroomDetails(Guid id)
        => HandleGetQuery(new GetClassroomQuery(id));
    [HttpPost]
    public Task<ActionResult<ClassroomDTO>> Post(CreateClassroomCommand command)
        => HandleCreateCommand(command, nameof(GetClassroomDetails));
    [HttpPut("{id}")]
    public Task<IActionResult> Put(Guid id, UpdateClassroomCommand request)
        => HandleUpdateCommand(id, request);
    [HttpDelete("{id}")]
    public Task<ActionResult<ClassroomDTO>> Delete(Guid id)
        => HandleDeleteCommand(new DeleteClassroomCommand(id));
    [HttpDelete]
    public Task<IActionResult> DeleteRange(IList<Guid> ids)
        => HandleDeleteRangeCommand(new DeleteRangeClassroomCommand(ids));
    [HttpGet("{id}/members")]
    public async Task<ActionResult<IEnumerable<MemberDTO>>> GetMembers(Guid id, [FromQuery] PaginationParams @params)
        => HandlePagedList(await Mediator.Send(new GetClassroomUsersWithPaginationQuery(id, @params)));
    [HttpPost("{id}/members/{userName}")]
    public async Task<IActionResult> AddMember(Guid id, string userName)
        => Ok(await Mediator.Send(new AddClassroomUserCommand(id, userName)));
    [HttpDelete("{id}/members/{userName}")]
    public async Task<IActionResult> RemoveMember(Guid id, string userName)
        => Ok(await Mediator.Send(new RemoveClassroomUserCommand(id, userName)));
    [HttpGet("{id}/exercises")]
    public async Task<ActionResult<IEnumerable<ClassroomExerciseDTO>>> GetExercises(Guid id, [FromQuery] PaginationParams @params)
        => HandlePagedList(await Mediator.Send(new GetClassroomExercisesWithPaginationQuery(id, @params)));
    [HttpGet("{id}/groups")]
    public async Task<ActionResult<IEnumerable<ClassroomGroupDTO>>> GetGroups(Guid id, [FromQuery] PaginationParams @params)
        => HandlePagedList(await Mediator.Send(new GetClassroomGroupsWithPaginationQuery(id, @params)));
    [HttpGet("{id}/posts")]
    public async Task<ActionResult<IEnumerable<ClassroomPostDTO>>> GetPosts(Guid id, [FromQuery] PaginationParams @params)
        => HandlePagedList(await Mediator.Send(new GetClassroomPostsWithPaginationQuery(id, @params)));
}
