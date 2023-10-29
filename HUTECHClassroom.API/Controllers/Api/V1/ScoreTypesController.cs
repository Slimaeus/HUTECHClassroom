using HUTECHClassroom.Application.Scores.DTOs;
using HUTECHClassroom.Application.Scores.Queries.GetScoreTypesWithPagination;

namespace HUTECHClassroom.API.Controllers.Api.V1;

[ApiVersion("1.0")]
public sealed class ScoreTypesController : BaseEntityApiController<int, ScoreTypeDTO>
{
    [HttpGet]
    public Task<ActionResult<IEnumerable<ScoreTypeDTO>>> Get([FromQuery] PaginationParams @params)
        => HandlePaginationQuery<GetScoreTypesWithPaginationQuery, PaginationParams>(new GetScoreTypesWithPaginationQuery(@params));
}
