using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Domain.Enums;

namespace HUTECHClassroom.Application.Comments;

public record CommentPaginationParams(int? PageNumber, int? PageSize, string? SearchString, Guid? UserId = null) : UserPaginationParams(PageNumber, PageSize, SearchString, UserId)
{
    public SortingOrder ContentOrder { get; set; }
}
