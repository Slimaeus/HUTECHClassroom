using Microsoft.AspNetCore.Mvc;

namespace HUTECHClassroom.API.Controllers.Api
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
    }
}
