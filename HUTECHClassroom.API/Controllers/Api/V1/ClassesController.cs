using HUTECHClassroom.Application.Classes.Commands.CreateClass;
using HUTECHClassroom.Application.Classes.DTOs;
using HUTECHClassroom.Application.Classes.Queries.GetClass;
using HUTECHClassroom.Application.Classes.Queries.GetClassesWithPaginationn;
using HUTECHClassroom.Application.Classs.Commands.DeleteClass;
using HUTECHClassroom.Application.Classs.Commands.DeleteRangeClass;
using HUTECHClassroom.Application.Classs.Commands.UpdateClass;

namespace HUTECHClassroom.API.Controllers.Api.V1;

[ApiVersion("1.0")]
public sealed class ClassesController : BaseEntityApiController<ClassDTO>
{
    [HttpGet]
    public Task<ActionResult<IEnumerable<ClassDTO>>> Get([FromQuery] PaginationParams paginationParams)
        => HandlePaginationQuery<GetClassesWithPaginationQuery, PaginationParams>(new GetClassesWithPaginationQuery(paginationParams));

    [HttpGet("{classId}")]
    public Task<ActionResult<ClassDTO>> GetClassDetails(Guid classId)
        => HandleGetQuery(new GetClassQuery(classId));

    [Authorize(CreateClassPolicy)]
    [HttpPost]
    public Task<ActionResult<ClassDTO>> Post(CreateClassCommand command)
        => HandleCreateCommand<CreateClassCommand, GetClassQuery>(command);

    [Authorize(UpdateClassPolicy)]
    [HttpPut("{classId}")]
    public Task<IActionResult> Put(Guid classId, UpdateClassCommand request)
        => HandleUpdateCommand(classId, request);

    [Authorize(DeleteClassPolicy)]
    [HttpDelete("{classId}")]
    public Task<ActionResult<ClassDTO>> Delete(Guid classId)
        => HandleDeleteCommand(new DeleteClassCommand(classId));

    [Authorize(DeleteClassPolicy)]
    [HttpDelete]
    public Task<IActionResult> DeleteRange(IList<Guid> classIds)
        => HandleDeleteRangeCommand(new DeleteRangeClassCommand(classIds));
}
