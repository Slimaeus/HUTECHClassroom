namespace HUTECHClassroom.Application.Common.Models;

public record UserPaginationParams(
    Guid UserId,
    int? PageNumber,
    int? PageSize,
    string SearchString
) : PaginationParams(PageNumber, PageSize, SearchString);
