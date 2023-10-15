using HUTECHClassroom.Domain.Interfaces;
using HUTECHClassroom.Domain.Models.ComputerVision;
using System.Net;

namespace HUTECHClassroom.API.Controllers.Api.V1;

[ApiVersion("1.0")]
public class FeaturesController : BaseApiController
{
    private readonly IAzureComputerVisionService _azureComputerVisionService;

    public FeaturesController(IAzureComputerVisionService azureComputerVisionService)
    {
        _azureComputerVisionService = azureComputerVisionService;
    }

    [HttpGet("vision/read/{url}")]
    public async Task<ActionResult<IEnumerable<OptimizedPage>>> Read(string url)
    {
        var decodedUrl = WebUtility.UrlDecode(url);
        return Ok(await _azureComputerVisionService.ReadFileUrl(decodedUrl));
    }
}
