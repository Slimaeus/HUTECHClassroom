using HUTECHClassroom.Application.Common.DTOs;
using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Exercises.Commands.AddExerciseUser;
using HUTECHClassroom.Application.Exercises.Commands.CreateExercise;
using HUTECHClassroom.Application.Exercises.Commands.DeleteExercise;
using HUTECHClassroom.Application.Exercises.Commands.DeleteRangeExercise;
using HUTECHClassroom.Application.Exercises.Commands.RemoveExerciseUser;
using HUTECHClassroom.Application.Exercises.Commands.UpdateGroup;
using HUTECHClassroom.Application.Exercises.DTOs;
using HUTECHClassroom.Application.Exercises.Queries.GetExercise;
using HUTECHClassroom.Application.Exercises.Queries.GetExercisesWithPagination;
using HUTECHClassroom.Application.Exercises.Queries.GetExerciseUsersWithPagination;
using Microsoft.AspNetCore.Mvc;

namespace HUTECHClassroom.API.Controllers.Api.V1;

[ApiVersion("1.0")]
public class ExercisesController : BaseEntityApiController<ExerciseDTO>
{
    [HttpGet]
    public Task<ActionResult<IEnumerable<ExerciseDTO>>> Get([FromQuery] PaginationParams @params)
        => HandlePaginationQuery<GetExercisesWithPaginationQuery, PaginationParams>(new GetExercisesWithPaginationQuery(@params));
    [HttpGet("{id}", Name = nameof(GetExerciseDetails))]
    public Task<ActionResult<ExerciseDTO>> GetExerciseDetails(Guid id)
        => HandleGetQuery(new GetExerciseQuery(id));
    [HttpPost]
    public Task<ActionResult<ExerciseDTO>> Post(CreateExerciseCommand command)
        => HandleCreateCommand(command, nameof(GetExerciseDetails));
    [HttpPut("{id}")]
    public Task<IActionResult> Put(Guid id, UpdateExerciseCommand request)
        => HandleUpdateCommand(id, request);
    [HttpDelete("{id}")]
    public Task<ActionResult<ExerciseDTO>> Delete(Guid id)
        => HandleDeleteCommand(new DeleteExerciseCommand(id));
    [HttpDelete]
    public Task<IActionResult> DeleteRange(IList<Guid> ids)
        => HandleDeleteRangeCommand(new DeleteRangeExerciseCommand(ids));
    [HttpGet("{id}/members")]
    public async Task<ActionResult<IEnumerable<MemberDTO>>> GetMembers(Guid id, [FromQuery] PaginationParams @params)
        => HandlePagedList(await Mediator.Send(new GetExerciseUsersWithPaginationQuery(id, @params)));
    [HttpPost("{id}/members/{userName}")]
    public async Task<IActionResult> AddMember(Guid id, string userName)
        => Ok(await Mediator.Send(new AddExerciseUserCommand(id, userName)));
    [HttpDelete("{id}/members/{userName}")]
    public async Task<IActionResult> RemoveMember(Guid id, string userName)
        => Ok(await Mediator.Send(new RemoveExerciseUserCommand(id, userName)));
}
