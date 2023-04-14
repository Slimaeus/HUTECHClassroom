using EntityFrameworkCore.Repository.Collections;
using HUTECHClassroom.Application.Common.DTOs;
using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Missions.Commands.CreateMission;
using HUTECHClassroom.Application.Missions.Commands.DeleteCommand;
using HUTECHClassroom.Application.Missions.Commands.UpdateMission;
using HUTECHClassroom.Application.Missions.DTOs;
using HUTECHClassroom.Application.Missions.Queries.GetMission;
using HUTECHClassroom.Application.Missions.Queries.GetMissionsWithPagination;
using HUTECHClassroom.Application.Missions.Queries.GetMissionUsersWithPagination;
using Microsoft.AspNetCore.Mvc;

namespace HUTECHClassroom.API.Controllers.Api.V1
{
    [ApiVersion("1.0")]
    public class MissionsController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MissionDTO>>> Get([FromQuery] PaginationParams @params)
            => HandlePagedList(await Mediator.Send(new GetMissionsWithPaginationQuery(@params)));
        [HttpGet("{id}", Name = nameof(GetDetails))]
        public async Task<ActionResult<MissionDTO>> GetDetails(Guid id)
            => Ok(await Mediator.Send(new GetMissionQuery(id)));
        [HttpPost]
        public async Task<ActionResult<MissionDTO>> Post(CreateMissionCommand request)
        {
            var missionDTO = await Mediator.Send(request);
            return CreatedAtRoute(nameof(GetDetails), new { id = missionDTO.Id }, missionDTO);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, UpdateMissionCommand request)
        {
            if (id != request.Id)
            {
                ModelState.AddModelError("Id", "Ids are not the same");
                return ValidationProblem();
            }
            await Mediator.Send(request);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await Mediator.Send(new DeleteMissionCommand(id)));
        }
        [HttpGet("{id}/members")]
        public async Task<ActionResult<IEnumerable<MemberDTO>>> GetMembers(Guid id, [FromQuery] PaginationParams @params)
        {
            return HandlePagedList(await Mediator.Send(new GetMissionUsersWithPaginationQuery(id, @params)));
        }
    }
}
