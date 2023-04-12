using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HUTECHClassroom.API.Controllers.Api
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator  => _mediator ?? HttpContext.RequestServices.GetRequiredService<IMediator>();
    }
}
