using EntityFrameworkCore.Repository.Collections;
using HUTECHClassroom.Application.Missions.Queries;
using HUTECHClassroom.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HUTECHClassroom.API.Controllers.Api.V1
{
    [ApiVersion("1.0")]
    public class MissionsController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<IPagedList<Mission>>> Get([FromQuery] GetMissionsWithPaginationQuery request)
        {
            return Ok(await Mediator.Send(request));
        }
    }
}
