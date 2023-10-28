using HUTECHClassroom.Domain.Interfaces;

namespace HUTECHClassroom.API.Controllers.Api.V1;

[ApiVersion("1.0")]
[AllowAnonymous]
public class ResultsController : BaseApiController
{
    private readonly IPhotoAccessor _photoAccessor;

    public ResultsController(IPhotoAccessor photoAccessor)
        => _photoAccessor = photoAccessor;

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

    [HttpGet("internal-server-error")]
    public IActionResult GetInternalServerError()
        => StatusCode(StatusCodes.Status500InternalServerError);

    [HttpPost("add-photo")]
    public async Task<IActionResult> AddPhotoAsync(IFormFile file)
    {
        var result = await _photoAccessor.AddPhoto(file);
        return result switch
        {
            { IsSuccess: true } => Ok(result.Data.PublicId),
            _ => BadRequest(result)
        };
        //return Ok(result.PublicId);
    }

    [HttpDelete("delete-photo")]
    public async Task<IActionResult> DeletePhotoAsync(string publicId)
    {
        var result = await _photoAccessor.DeletePhoto(publicId);
        return result switch
        {
            { IsSuccess: true } => Ok(result),
            _ => BadRequest(result)
        };
    }

    [HttpGet("test")]
    public IActionResult Test()
        => Ok();
}
