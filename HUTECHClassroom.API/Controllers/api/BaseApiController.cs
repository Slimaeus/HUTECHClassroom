using EntityFrameworkCore.Repository.Collections;
using HUTECHClassroom.API.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HUTECHClassroom.API.Controllers.Api
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator  => _mediator ?? HttpContext.RequestServices.GetRequiredService<IMediator>();

        protected ActionResult HandlePagedList<T>(IPagedList<T> pagedList)
        {
            Response.Headers.Add("pagination", JsonConvert.SerializeObject(new { currentPage = pagedList.PageIndex, itemsPerPage = pagedList.PageSize, totalItems = pagedList.TotalCount, totalPages = pagedList.TotalPages, hasNext = pagedList.HasNextPage, hasPrevious = pagedList.HasPreviousPage }));
            return Ok(pagedList.Items);
        }
    }
}
