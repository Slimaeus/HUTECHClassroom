using Microsoft.AspNetCore.Mvc;

namespace HUTECHClassroom.API.Controllers.Api.V1
{
    [ApiVersion("1.0")]
    public class ResultsController : BaseApiController
    {
        [HttpGet]
        public IActionResult GetNotFound()
        {
            return NotFound();
        }
    }
}
