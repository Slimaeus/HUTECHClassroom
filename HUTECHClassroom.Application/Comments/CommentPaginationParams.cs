using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Domain.Enums;

namespace HUTECHClassroom.Application.Comments;

public record CommentPaginationParams(int? PageNumber, int? PageSize, string SearchString = "") : PaginationParams(PageNumber, PageSize, SearchString)
{
    public SortingOrder ContentOrder { get; set; }
}
