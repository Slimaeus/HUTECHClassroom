using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Domain.Enums;

namespace HUTECHClassroom.Application.Comments;

public record CommentPaginationParams(Guid UserId, int? PageNumber, int? PageSize, string SearchString) : UserPaginationParams(UserId, PageNumber, PageSize, SearchString)
{
    public SortingOrder ContentOrder { get; set; }
}
