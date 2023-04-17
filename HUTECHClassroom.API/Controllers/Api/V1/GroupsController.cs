using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Groups.Commands.CreateGroup;
using HUTECHClassroom.Application.Groups.DTOs;
using HUTECHClassroom.Application.Groups.Queries.GetGroup;
using HUTECHClassroom.Application.Groups.Queries.GetGroupsWithPagination;
using HUTECHClassroom.Application.Groups.Commands.DeleteGroup;
using HUTECHClassroom.Application.Groups.Commands.UpdateGroup;
using Microsoft.AspNetCore.Mvc;

namespace HUTECHClassroom.API.Controllers.Api.V1
{
    [ApiVersion("1.0")]
    public class GroupsController : BaseEntityApiController<GroupDTO>
    {
        [HttpGet]
        public Task<ActionResult<IEnumerable<GroupDTO>>> Get([FromQuery] PaginationParams @params)
            => HandlePaginationQuery(new GetGroupsWithPaginationQuery(@params));
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
    }
}
