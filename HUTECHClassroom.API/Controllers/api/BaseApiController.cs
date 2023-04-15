using EntityFrameworkCore.Repository.Collections;
using HUTECHClassroom.Application.Common.Interfaces;
using HUTECHClassroom.Application.Common.Requests;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HUTECHClassroom.API.Controllers.Api
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        protected IMediator Mediator => HttpContext.RequestServices.GetRequiredService<IMediator>();

        protected ActionResult<IEnumerable<T>> HandlePagedList<T>(IPagedList<T> pagedList)
        {
            Response.Headers.Add("pagination", JsonConvert.SerializeObject(new { currentPage = pagedList.PageIndex, itemsPerPage = pagedList.PageSize, totalItems = pagedList.TotalCount, totalPages = pagedList.TotalPages, hasNext = pagedList.HasNextPage, hasPrevious = pagedList.HasPreviousPage }));
            return Ok(pagedList.Items);
        }
        
    }
}
