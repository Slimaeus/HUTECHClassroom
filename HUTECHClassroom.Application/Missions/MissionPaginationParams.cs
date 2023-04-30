using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Domain.Enums;

namespace HUTECHClassroom.Application.Missions;

public record MissionPaginationParams(int? PageNumber, int? PageSize, string SearchString) : PaginationParams(PageNumber, PageSize, SearchString)
{
    public SortingOrder TitleOrder { get; set; }
    public SortingOrder DescriptionOrder { get; set; }
    public SortingOrder IsDoneOrder { get; set; }
}
