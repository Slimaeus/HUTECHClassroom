using Microsoft.AspNetCore.Mvc;

namespace HUTECHClassroom.API.Controllers.Api.V1;

[ApiVersion("1.0")]
public class ResultsController : BaseApiController
{
    [HttpGet("not-found")]
    public IActionResult GetNotFound()
        => NotFound();
    [HttpGet("ok")]
    public IActionResult GetOk()
        => Ok();
    [HttpGet("bad-request")]
    public IActionResult GetBadRequest()
                => BadRequest();
    [HttpGet("no-content")]
    public IActionResult GetNoContent()
                => NoContent();
    [HttpGet("unauthorized")]
    public IActionResult GetUnauthorized()
                => Unauthorized();
    [HttpGet("conflict")]
    public IActionResult GetConflict()
                => Conflict();
    [HttpGet("forbid")]
    public IActionResult GetForbid()
                => Forbid();
    [HttpGet("InternalServerError")]
    public IActionResult GetInternalServerError()
                => StatusCode(StatusCodes.Status500InternalServerError);
}
