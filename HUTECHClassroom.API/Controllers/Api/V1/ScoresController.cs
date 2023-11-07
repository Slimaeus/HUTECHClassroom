using HUTECHClassroom.Application.Scores.Commands.AddStudentResult;

namespace HUTECHClassroom.API.Controllers.Api.V1;

[Authorize(Policy = NeedRolePolicy)]
[ApiVersion("1.0")]
public sealed class ScoresController : BaseApiController
{
    [HttpPost("add-result")]
    public async Task<IActionResult> AddResutl(AddStudentResultCommand request)
    {
        await Mediator.Send(request);
        return NoContent();
    }
}
