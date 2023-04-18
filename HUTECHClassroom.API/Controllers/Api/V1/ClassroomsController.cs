using HUTECHClassroom.Application.Classrooms.DTOs;
using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Classrooms.Queries.GetClassroomsWithPagination;
using Microsoft.AspNetCore.Mvc;

namespace HUTECHClassroom.API.Controllers.Api.V1
{
    [ApiVersion("1.0")]
    public class ClassroomsController : BaseEntityApiController<ClassroomDTO>
    {
        [HttpGet]
        public Task<ActionResult<IEnumerable<ClassroomDTO>>> Get([FromQuery] PaginationParams @params)
            => HandlePaginationQuery(new GetClassroomsWithPaginationQuery(@params));
    }
}
