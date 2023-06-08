using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Domain.Enums;

namespace HUTECHClassroom.Application.Exercises;

public record ExercisePaginationParams(int? PageNumber, int? PageSize, string SearchString) : PaginationParams(PageNumber, PageSize, SearchString)
{
    public Guid UserId { get; set; }
    public SortingOrder TitleOrder { get; set; }
    public SortingOrder InstructionOrder { get; set; }
    public SortingOrder LinkOrder { get; set; }
    public SortingOrder TotalScoreOrder { get; set; }
    public SortingOrder DeadlineOrder { get; set; }
    public SortingOrder TopicOrder { get; set; }
    public SortingOrder CriteriaOrder { get; set; }
}
