using HUTECHClassroom.Application.Classes.Commands.CreateClass;
using HUTECHClassroom.Application.Classes.DTOs;
using HUTECHClassroom.Application.Classes.Queries.GetClass;
using HUTECHClassroom.Application.Classes.Queries.GetClassesWithPaginationn;

namespace HUTECHClassroom.API.Controllers.Api.V1;

[ApiVersion("1.0")]
public class ClassesController : BaseEntityApiController<string, ClassDTO>
{
    [HttpGet]
    public Task<ActionResult<IEnumerable<ClassDTO>>> Get([FromQuery] PaginationParams paginationParams)
        => HandlePaginationQuery<GetClassesWithPaginationQuery, PaginationParams>(new GetClassesWithPaginationQuery(paginationParams));

    [HttpGet("{classId}")]
    public Task<ActionResult<ClassDTO>> GetClassDetails(string classId)
        => HandleGetQuery(new GetClassQuery(classId));

    [HttpPost]
    public Task<ActionResult<ClassDTO>> Post(CreateClassCommand command)
        => HandleCreateCommand<CreateClassCommand, GetClassQuery>(command);
}
