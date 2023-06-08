using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Domain.Enums;

namespace HUTECHClassroom.Application.Answers;

public record AnswerPaginationParams(Guid UserId, int? PageNumber, int? PageSize, string SearchString) : UserPaginationParams(UserId, PageNumber, PageSize, SearchString)
{
    public SortingOrder DescriptionOrder { get; set; }
    public SortingOrder LinkOrder { get; set; }
    public SortingOrder ScoreOrder { get; set; }

}
