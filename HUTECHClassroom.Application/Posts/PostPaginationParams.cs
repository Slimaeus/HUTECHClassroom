using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Domain.Enums;

namespace HUTECHClassroom.Application.Posts;

public record PostPaginationParams(int? PageNumber, int? PageSize, string SearchString) : PaginationParams(PageNumber, PageSize, SearchString)
{
    public SortingOrder ContentOrder { get; set; }
    public SortingOrder LinkOrder { get; set; }
}
