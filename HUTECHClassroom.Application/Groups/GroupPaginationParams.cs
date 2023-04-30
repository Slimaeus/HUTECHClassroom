using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Domain.Enums;

namespace HUTECHClassroom.Application.Groups;

public record GroupPaginationParams(int? PageNumber, int? PageSize, string SearchString) : PaginationParams(PageNumber, PageSize, SearchString)
{
    public SortingOrder NameOrder { get; set; }
    public SortingOrder DescriptionOrder { get; set; }
}
