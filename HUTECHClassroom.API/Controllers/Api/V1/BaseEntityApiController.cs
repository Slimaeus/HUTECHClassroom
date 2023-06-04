using HUTECHClassroom.Application.Common.DTOs;
using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Common.Requests;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace HUTECHClassroom.API.Controllers.Api.V1;

//[Authorize]
public class BaseEntityApiController<TKey, TEntityDTO> : BaseApiController
    where TEntityDTO : class, IEntityDTO<TKey>
{
    protected async Task<ActionResult<IEnumerable<TEntityDTO>>> HandlePaginationQuery<TPaginationQuery, TPaginationParams>(TPaginationQuery query)
        where TPaginationQuery : GetWithPaginationQuery<TEntityDTO, TPaginationParams>
        where TPaginationParams : PaginationParams
        => HandlePagedList(await Mediator.Send(query));

    protected async Task<ActionResult<TEntityDTO>> HandleGetQuery<TGetQuery>(TGetQuery query)
        where TGetQuery : GetQuery<TEntityDTO>
        => Ok(await Mediator.Send(query));

    protected async Task<ActionResult<TEntityDTO>> HandleCreateCommand<TCreateCommand, TGetQuery>(TCreateCommand command, Func<TKey, TGetQuery> getQuery)
        where TCreateCommand : CreateCommand<TKey>
        where TGetQuery : GetQuery<TEntityDTO>
    {
        var id = await Mediator.Send(command);
        var dto = await Mediator.Send(getQuery(id));

        var domain = HttpContext.Request.GetDisplayUrl();
        var routeTemplate = ControllerContext.ActionDescriptor.AttributeRouteInfo.Template;
        var apiVersion = HttpContext.GetRequestedApiVersion().ToString();

        return Created($"{domain}/{routeTemplate.Replace("{version:apiVersion}", apiVersion)}/{id}", dto);
    }

    protected async Task<IActionResult> HandleUpdateCommand<TUpdateCommand>(TKey id, TUpdateCommand command)
        where TUpdateCommand : UpdateCommand<TKey>
    {
        if (!id.Equals(command.Id))
        {
            ModelState.AddModelError("Id", "Ids are not the same");
            return ValidationProblem();
        }
        await Mediator.Send(command);
        return NoContent();
    }
    protected async Task<ActionResult<TEntityDTO>> HandleDeleteCommand<TDeleteCommand>(TDeleteCommand command)
        where TDeleteCommand : DeleteCommand<TKey, TEntityDTO>
        => Ok(await Mediator.Send(command));
    protected async Task<IActionResult> HandleDeleteRangeCommand<TDeleteRangeCommand>(TDeleteRangeCommand command)
        where TDeleteRangeCommand : DeleteRangeCommand<TKey>
    {
        await Mediator.Send(command);
        return NoContent();
    }
}

public class BaseEntityApiController<TEntityDTO> : BaseEntityApiController<Guid, TEntityDTO>
    where TEntityDTO : class, IEntityDTO<Guid>
{ }