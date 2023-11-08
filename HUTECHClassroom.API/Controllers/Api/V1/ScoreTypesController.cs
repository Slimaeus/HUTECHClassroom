using HUTECHClassroom.Application.Scores.DTOs;
using HUTECHClassroom.Application.Scores.Queries.GetScoreTypesWithPagination;
using HUTECHClassroom.Application.ScoreTypes.Commands.CreateScoreType;
using HUTECHClassroom.Application.ScoreTypes.Commands.DeleteRangeScoreType;
using HUTECHClassroom.Application.ScoreTypes.Commands.DeleteScoreType;
using HUTECHClassroom.Application.ScoreTypes.Commands.UpdateScoreType;
using HUTECHClassroom.Application.ScoreTypes.Queries.GetScoreType;

namespace HUTECHClassroom.API.Controllers.Api.V1;

[ApiVersion("1.0")]
public sealed class ScoreTypesController : BaseEntityApiController<int, ScoreTypeDTO>
{
    [Authorize(ReadScoreTypePolicy)]
    [HttpGet]
    public Task<ActionResult<IEnumerable<ScoreTypeDTO>>> Get([FromQuery] PaginationParams @params)
        => HandlePaginationQuery<GetScoreTypesWithPaginationQuery, PaginationParams>(new GetScoreTypesWithPaginationQuery(@params));
    [Authorize(ReadScoreTypePolicy)]
    [HttpGet("{scoreTypeId}")]
    public Task<ActionResult<ScoreTypeDTO>> GetScoreTypeDetails(int scoreTypeId)
        => HandleGetQuery(new GetScoreTypeQuery(scoreTypeId));

    [Authorize(CreateScoreTypePolicy)]
    [HttpPost]
    public Task<ActionResult<ScoreTypeDTO>> Post(CreateScoreTypeCommand request)
        => HandleCreateCommand<CreateScoreTypeCommand, GetScoreTypeQuery>(request);

    [Authorize(UpdateScoreTypePolicy)]
    [HttpPut("{scoreTypeId}")]
    public Task<IActionResult> Put(int scoreTypeId, UpdateScoreTypeCommand request)
        => HandleUpdateCommand(scoreTypeId, request);

    [Authorize(DeleteScoreTypePolicy)]
    [HttpDelete("{scoreTypeId}")]
    public Task<ActionResult<ScoreTypeDTO>> Delete(int scoreTypeId)
        => HandleDeleteCommand(new DeleteScoreTypeCommand(scoreTypeId));

    [Authorize(DeleteScoreTypePolicy)]
    [HttpDelete]
    public Task<IActionResult> DeleteRange(IList<int> scoreTypeIds)
        => HandleDeleteRangeCommand(new DeleteRangeScoreTypeCommand(scoreTypeIds));
}
