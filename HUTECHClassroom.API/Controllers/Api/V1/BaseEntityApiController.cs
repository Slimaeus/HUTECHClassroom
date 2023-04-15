using HUTECHClassroom.Application.Common.Interfaces;
using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Missions.Commands.CreateMission;
using HUTECHClassroom.Application.Missions.Commands.DeleteCommand;
using HUTECHClassroom.Application.Missions.Commands.UpdateMission;
using HUTECHClassroom.Application.Missions.DTOs;
using HUTECHClassroom.Application.Missions.Queries.GetMission;
using HUTECHClassroom.Application.Missions.Queries.GetMissionsWithPagination;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HUTECHClassroom.API.Controllers.Api.V1
{
    public class BaseEntityApiController<TEntityDTO> : BaseApiController
        where TEntityDTO : class, IEntityDTO
    {
        [HttpGet]
        public Task<ActionResult<IEnumerable<MissionDTO>>> Get([FromQuery] PaginationParams @params)
            => HandlePaginationQuery(new GetMissionsWithPaginationQuery(@params));
        [HttpGet("{id}", Name = nameof(GetDetails))]
        public Task<ActionResult<MissionDTO>> GetDetails(Guid id)
            => HandleGetQuery(new GetMissionQuery(id));
        [HttpPost]
        public Task<ActionResult<MissionDTO>> Post(CreateMissionCommand request)
            => HandleCreateCommand(request, nameof(GetDetails));
        [HttpPut("{id}")]
        public Task<IActionResult> Put(Guid id, UpdateMissionCommand request)
            => HandleUpdateCommand(id, request);
        [HttpDelete("{id}")]
        public Task<ActionResult<MissionDTO>> Delete(Guid id)
            => HandleDeleteCommand(new DeleteMissionCommand(id));
        protected async Task<ActionResult<IEnumerable<TEntityDTO>>> HandlePaginationQuery<TPaginationQuery>(TPaginationQuery query)
            where TPaginationQuery : GetWithPaginationQuery<TEntityDTO>
            => HandlePagedList(await Mediator.Send(query));

        protected async Task<ActionResult<TEntityDTO>> HandleGetQuery<TGetQuery>(TGetQuery query)
            where TGetQuery : GetQuery<TEntityDTO>
            => Ok(await Mediator.Send(query));

        protected async Task<ActionResult<TEntityDTO>> HandleCreateCommand<TCreateCommand>(TCreateCommand command, string routeName)
            where TCreateCommand : CreateCommand<TEntityDTO>
        {
            var dto = await Mediator.Send(command);
            return CreatedAtRoute(routeName, new { id = dto.Id }, dto);
        }

        protected async Task<IActionResult> HandleUpdateCommand<TUpdateCommand>(Guid id, TUpdateCommand command)
            where TUpdateCommand : UpdateCommand
        {
            if (id != command.Id)
            {
                ModelState.AddModelError("Id", "Ids are not the same");
                return ValidationProblem();
            }
            await Mediator.Send(command);
            return NoContent();
        }
        protected async Task<ActionResult<TEntityDTO>> HandleDeleteCommand<TDeleteCommand>(TDeleteCommand command)
            where TDeleteCommand : DeleteCommand<TEntityDTO>
            => Ok(await Mediator.Send(command));
    }
}
