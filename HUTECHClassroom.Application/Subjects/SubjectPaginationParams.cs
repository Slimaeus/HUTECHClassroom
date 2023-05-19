using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Domain.Enums;

namespace HUTECHClassroom.Application.Subjects;

public record SubjectPaginationParams(int? PageNumber, int? PageSize, string SearchString) : PaginationParams(PageNumber, PageSize, SearchString)
{
    public SortingOrder CodeOrder { get; set; }
    public SortingOrder TitleOrder { get; set; }
    public SortingOrder TotalCreditsOrder { get; set; }
}
