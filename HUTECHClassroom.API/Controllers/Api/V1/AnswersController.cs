using HUTECHClassroom.Application.Answers.Commands.CreateAnswer;
using HUTECHClassroom.Application.Answers.Commands.DeleteAnswer;
using HUTECHClassroom.Application.Answers.Commands.DeleteRangeAnswer;
using HUTECHClassroom.Application.Answers.Commands.UpdateAnswer;
using HUTECHClassroom.Application.Answers.DTOs;
using HUTECHClassroom.Application.Answers.Queries.GetAnswer;
using HUTECHClassroom.Application.Answers.Queries.GetAnswersWithPagination;
using HUTECHClassroom.Application.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace HUTECHClassroom.API.Controllers.Api.V1;

[ApiVersion("1.0")]
public class AnswersController : BaseEntityApiController<AnswerDTO>
{
    [HttpGet]
    public Task<ActionResult<IEnumerable<AnswerDTO>>> Get([FromQuery] PaginationParams @params)
        => HandlePaginationQuery<GetAnswersWithPaginationQuery, PaginationParams>(new GetAnswersWithPaginationQuery(@params));
    [HttpGet("{id}", Name = nameof(GetAnswerDetails))]
    public Task<ActionResult<AnswerDTO>> GetAnswerDetails(Guid id)
        => HandleGetQuery(new GetAnswerQuery(id));
    [HttpPost]
    public Task<ActionResult<AnswerDTO>> Answer(CreateAnswerCommand command)
        => HandleCreateCommand(command, nameof(GetAnswerDetails));
    [HttpPut("{id}")]
    public Task<IActionResult> Put(Guid id, UpdateAnswerCommand request)
        => HandleUpdateCommand(id, request);
    [HttpDelete("{id}")]
    public Task<ActionResult<AnswerDTO>> Delete(Guid id)
        => HandleDeleteCommand(new DeleteAnswerCommand(id));
    [HttpDelete]
    public Task<IActionResult> DeleteRange(IList<Guid> ids)
        => HandleDeleteRangeCommand(new DeleteRangeAnswerCommand(ids));
}
