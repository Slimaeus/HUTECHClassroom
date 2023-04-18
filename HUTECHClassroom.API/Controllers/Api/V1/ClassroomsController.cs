using HUTECHClassroom.Application.Classrooms.DTOs;
using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Classrooms.Queries.GetClassroomsWithPagination;
using Microsoft.AspNetCore.Mvc;
using HUTECHClassroom.Application.Classrooms.Queries.GetClassroom;
using HUTECHClassroom.Application.Classrooms.Commands.CreateClassroom;
using HUTECHClassroom.Application.Classrooms.Commands.DeleteClassroom;
using HUTECHClassroom.Application.Classrooms.Commands.UpdateClassroom;
using HUTECHClassroom.Application.Common.DTOs;
using HUTECHClassroom.Application.Classrooms.Queries.GetClassroomUsersWithPagination;
using HUTECHClassroom.Application.Classrooms.Commands.AddClassroomUser;
using HUTECHClassroom.Application.Classrooms.Commands.RemoveClassroomUser;

namespace HUTECHClassroom.API.Controllers.Api.V1
{
    [ApiVersion("1.0")]
    public class ClassroomsController : BaseEntityApiController<ClassroomDTO>
    {
        [HttpGet]
        public Task<ActionResult<IEnumerable<ClassroomDTO>>> Get([FromQuery] PaginationParams @params)
            => HandlePaginationQuery(new GetClassroomsWithPaginationQuery(@params));
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
        [HttpGet("{id}/members")]
        public async Task<ActionResult<IEnumerable<MemberDTO>>> GetMembers(Guid id, [FromQuery] PaginationParams @params)
            => HandlePagedList(await Mediator.Send(new GetClassroomUsersWithPaginationQuery(id, @params)));
        [HttpPost("{id}/members/{userName}")]
        public async Task<IActionResult> AddMember(Guid id, string userName)
            => Ok(await Mediator.Send(new AddClassroomUserCommand(id, userName)));
        [HttpDelete("{id}/members/{userName}")]
        public async Task<IActionResult> RemoveMember(Guid id, string userName)
            => Ok(await Mediator.Send(new RemoveClassroomUserCommand(id, userName)));
    }
}
