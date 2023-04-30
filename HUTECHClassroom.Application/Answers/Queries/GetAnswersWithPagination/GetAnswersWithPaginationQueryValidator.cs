using HUTECHClassroom.Application.Answers.DTOs;
using HUTECHClassroom.Application.Common.Validators;

namespace HUTECHClassroom.Application.Answers.Queries.GetAnswersWithPagination;

public class GetAnswersWithPaginationQueryValidator : GetWithPaginationQueryValidator<GetAnswersWithPaginationQuery, AnswerDTO, AnswerPaginationParams>
{
    public GetAnswersWithPaginationQueryValidator()
    {
    }
}
