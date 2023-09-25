namespace HUTECHClassroom.API.Controllers.Api.V1;

[ApiVersion("1.0")]
public class ExercisesController : BaseEntityApiController<ExerciseDTO>
{
    [Authorize(ReadExercisePolicy)]
    [HttpGet]
    public Task<ActionResult<IEnumerable<ExerciseDTO>>> Get([FromQuery] ExercisePaginationParams @params)
        => HandlePaginationQuery<GetExercisesWithPaginationQuery, ExercisePaginationParams>(new GetExercisesWithPaginationQuery(@params));
    [Authorize(ReadExercisePolicy)]
    [HttpGet("{exerciseId}")]
    public Task<ActionResult<ExerciseDTO>> GetExerciseDetails(Guid exerciseId)
        => HandleGetQuery(new GetExerciseQuery(exerciseId));
    [Authorize(CreateExercisePolicy)]
    [HttpPost]
    public Task<ActionResult<ExerciseDTO>> Post(CreateExerciseCommand command)
        => HandleCreateCommand(command, exerciseId => new GetExerciseQuery(exerciseId));
    [Authorize(UpdateExercisePolicy)]
    [HttpPut("{exerciseId}")]
    public Task<IActionResult> Put(Guid exerciseId, UpdateExerciseCommand request)
        => HandleUpdateCommand(exerciseId, request);
    [Authorize(DeleteExercisePolicy)]
    [HttpDelete("{exerciseId}")]
    public Task<ActionResult<ExerciseDTO>> Delete(Guid exerciseId)
        => HandleDeleteCommand(new DeleteExerciseCommand(exerciseId));
    [Authorize(DeleteExercisePolicy)]
    [HttpDelete]
    public Task<IActionResult> DeleteRange(IList<Guid> exerciseIds)
        => HandleDeleteRangeCommand(new DeleteRangeExerciseCommand(exerciseIds));
    [Authorize(ReadExercisePolicy)]
    [HttpGet("{exerciseId}/answers")]
    public async Task<ActionResult<IEnumerable<AnswerDTO>>> GetAnswers(Guid exerciseId, [FromQuery] ExercisePaginationParams @params)
        => HandlePagedList(await Mediator.Send(new GetExerciseAnswersWithPaginationQuery(exerciseId, @params)));
    [Authorize(ReadExercisePolicy)]
    [HttpGet("{exerciseId}/members")]
    public async Task<ActionResult<IEnumerable<ExerciseUserDTO>>> GetMembers(Guid exerciseId, [FromQuery] PaginationParams @params)
        => HandlePagedList(await Mediator.Send(new GetExerciseUsersWithPaginationQuery(exerciseId, @params)));
    [Authorize(UpdateExercisePolicy)]
    [HttpPost("{exerciseId}/members/{userId}")]
    public async Task<IActionResult> AddMember(Guid exerciseId, Guid userId)
        => Ok(await Mediator.Send(new AddExerciseUserCommand(exerciseId, userId)));
    [Authorize(UpdateExercisePolicy)]
    [HttpDelete("{exerciseId}/members/{userId}")]
    public async Task<IActionResult> RemoveMember(Guid exerciseId, Guid userId)
        => Ok(await Mediator.Send(new RemoveExerciseUserCommand(exerciseId, userId)));
}
