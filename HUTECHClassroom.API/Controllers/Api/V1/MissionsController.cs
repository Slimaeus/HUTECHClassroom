using EntityFrameworkCore.Repository.Collections;
using HUTECHClassroom.Application.Missions.Commands.CreateMission;
using HUTECHClassroom.Application.Missions.Commands.DeleteCommand;
using HUTECHClassroom.Application.Missions.Commands.UpdateMission;
using HUTECHClassroom.Application.Missions.DTOs;
using HUTECHClassroom.Application.Missions.Queries;
using Microsoft.AspNetCore.Mvc;

namespace HUTECHClassroom.API.Controllers.Api.V1
{
    [ApiVersion("1.0")]
    public class MissionsController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<IPagedList<MissionDTO>>> Get([FromQuery] GetMissionsWithPaginationQuery request)
            => HandlePagedList(await Mediator.Send(request));
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
    }
}
