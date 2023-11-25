using HUTECHClassroom.Application.Scores.Commands.AddStudentResult;
using HUTECHClassroom.Application.Scores.DTOs;
using HUTECHClassroom.Application.Scores.Queries.GetStudentScoresFromFile;

namespace HUTECHClassroom.API.Controllers.Api.V1;

[Authorize(Policy = NeedRolePolicy)]
[ApiVersion("1.0")]
public sealed class ScoresController : BaseApiController
{
    [HttpPost("ScanResult")]
    public async Task<ActionResult<IEnumerable<StudentResultDTO>>> ScanResult(IFormFile file)
    {
        return Ok(await Mediator.Send(new GetStudentScoresFromFileQuery(file)));
    }

    [HttpPost("add-result")]
    public async Task<IActionResult> AddResutl(AddStudentResultCommand request)
    {
        await Mediator.Send(request);
        return NoContent();
    }
}
