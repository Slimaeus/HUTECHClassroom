using EntityFrameworkCore.Repository.Collections;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Missions.DTOs;
using HUTECHClassroom.Application.Missions.Queries;
using HUTECHClassroom.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HUTECHClassroom.API.Controllers.Api.V1
{
    [ApiVersion("1.0")]
    public class MissionsController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<IPagedList<MissionDTO>>> Get([FromQuery] GetMissionsWithPaginationQuery request)
        {
            return HandlePagedList(await Mediator.Send(request));
        }

        [HttpGet("test")]
        public async Task<ActionResult<IPagedList<MissionDTO>>> Get([FromQuery] GetWithPaginationQuery<Mission, MissionDTO> request)
        {
            return HandlePagedList(await Mediator.Send(request));
        }
    }
}
