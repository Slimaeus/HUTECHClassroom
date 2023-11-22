using HUTECHClassroom.Application.Classrooms.Queries.GetClassroomResultsWithPagination;
using HUTECHClassroom.Application.Classrooms.Queries.GetClassroomStudentResultsWithPagination;
using HUTECHClassroom.Application.Scores.DTOs;

namespace HUTECHClassroom.API.Controllers.Api.V1;

[ApiVersion("1.0")]
public sealed class ClassroomsController : BaseEntityApiController<ClassroomDTO>
{
    [Authorize(ReadClassroomPolicy)]
    [HttpGet]
    public Task<ActionResult<IEnumerable<ClassroomDTO>>> Get([FromQuery] ClassroomPaginationParams @params)
        => HandlePaginationQuery<GetClassroomsWithPaginationQuery, ClassroomPaginationParams>(new GetClassroomsWithPaginationQuery(@params));

    [Authorize(ReadClassroomPolicy)]
    [HttpGet("{classroomId}")]
    public Task<ActionResult<ClassroomDTO>> GetClassroomDetails(Guid classroomId)
        => HandleGetQuery(new GetClassroomQuery(classroomId));

    [Authorize(CreateClassroomPolicy)]
    [HttpPost]
    public Task<ActionResult<ClassroomDTO>> Post(CreateClassroomCommand command)
        => HandleCreateCommand<CreateClassroomCommand, GetClassroomQuery>(command);

    [Authorize(UpdateClassroomPolicy)]
    [HttpPut("{classroomId}")]
    public Task<IActionResult> Put(Guid classroomId, UpdateClassroomCommand request)
        => HandleUpdateCommand(classroomId, request);

    [Authorize(DeleteClassroomPolicy)]
    [HttpDelete("{classroomId}")]
    public Task<ActionResult<ClassroomDTO>> Delete(Guid classroomId)
        => HandleDeleteCommand(new DeleteClassroomCommand(classroomId));

    [Authorize(DeleteClassroomPolicy)]
    [HttpDelete]
    public Task<IActionResult> DeleteRange(IList<Guid> classroomIds)
        => HandleDeleteRangeCommand(new DeleteRangeClassroomCommand(classroomIds));

    [HttpGet("{classroomId}/members")]
    public async Task<ActionResult<IEnumerable<ClassroomUserDTO>>> GetMembers(Guid classroomId, [FromQuery] PaginationParams @params)
        => HandlePagedList(await Mediator.Send(new GetClassroomUsersWithPaginationQuery(classroomId, @params)));

    [Authorize(AddClassroomUserPolicy)]
    [HttpPost("{classroomId}/members/{userId}")]
    public async Task<IActionResult> AddMember(Guid classroomId, Guid userId)
        => Ok(await Mediator.Send(new AddClassroomUserCommand(classroomId, userId)));

    [Authorize(RemoveClassroomUserPolicy)]
    [HttpDelete("{classroomId}/members/{userId}")]
    public async Task<IActionResult> RemoveMember(Guid classroomId, Guid userId)
        => Ok(await Mediator.Send(new RemoveClassroomUserCommand(classroomId, userId)));

    [Authorize(ReadClassroomPolicy)]
    [HttpGet("{classroomId}/exercises")]
    public async Task<ActionResult<IEnumerable<ExerciseDTO>>> GetExercises(Guid classroomId, [FromQuery] ClassroomPaginationParams @params)
        => HandlePagedList(await Mediator.Send(new GetClassroomExercisesWithPaginationQuery(classroomId, @params)));

    [Authorize(ReadClassroomPolicy)]
    [HttpGet("{classroomId}/groups")]
    public async Task<ActionResult<IEnumerable<GroupDTO>>> GetGroups(Guid classroomId, [FromQuery] ClassroomPaginationParams @params)
        => HandlePagedList(await Mediator.Send(new GetClassroomGroupsWithPaginationQuery(classroomId, @params)));

    [Authorize(ReadClassroomPolicy)]
    [HttpGet("{classroomId}/posts")]
    public async Task<ActionResult<IEnumerable<PostDTO>>> GetPosts(Guid classroomId, [FromQuery] ClassroomPaginationParams @params)
        => HandlePagedList(await Mediator.Send(new GetClassroomPostsWithPaginationQuery(classroomId, @params)));

    [Authorize(ReadClassroomPolicy)]
    [HttpGet("{classroomId}/results/{scoreTypeId}")]
    public async Task<ActionResult<IEnumerable<StudentResultDTO>>> GetStudentResultByScoreType(Guid classroomId, int scoreTypeId, [FromQuery] PaginationParams @params)
        => HandlePagedList(await Mediator.Send(new GetClassroomResultsWithPaginationQuery(classroomId, scoreTypeId, @params)));

    [Authorize(ReadClassroomPolicy)]
    [HttpGet("{classroomId}/results")]
    public async Task<ActionResult<IEnumerable<StudentResultDTO>>> GetStudentResults(Guid classroomId, [FromQuery] PaginationParams @params)
        => HandlePagedList(await Mediator.Send(new GetClassroomResultsWithPaginationQuery(classroomId, default, @params)));

    //[Authorize(ReadClassroomPolicy)]
    //[HttpGet("{classroomId}/ClassroomScores/{scoreTypeId}")]
    //public async Task<ActionResult<IEnumerable<ClassroomStudentResultDTO>>> GetClassroomStudentResultByScoreType(Guid classroomId, int scoreTypeId, [FromQuery] PaginationParams @params)
    //    => HandlePagedList(await Mediator.Send(new GetClassroomStudentResultsWithPaginationQuery(classroomId, scoreTypeId, @params)));

    [Authorize(ReadClassroomPolicy)]
    [HttpGet("{classroomId}/Scores")]
    public async Task<ActionResult<IEnumerable<ClassroomStudentResultDTO>>> GetClassroomStudentResults(Guid classroomId, [FromQuery] PaginationParams @params)
        => HandlePagedList(await Mediator.Send(new GetClassroomStudentResultsWithPaginationQuery(classroomId, default, @params)));
}
