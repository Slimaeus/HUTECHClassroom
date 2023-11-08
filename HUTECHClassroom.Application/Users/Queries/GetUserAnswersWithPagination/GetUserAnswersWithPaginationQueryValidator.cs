using HUTECHClassroom.Application.Answers.DTOs;
using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Common.Validators;

namespace HUTECHClassroom.Application.Users.Queries.GetUserAnswersWithPagination;

public sealed class GetUserAnswersWithPaginationQueryValidator : GetWithPaginationQueryValidator<GetUserAnswersWithPaginationQuery, AnswerDTO, PaginationParams>
{
}
