namespace HUTECHClassroom.Application.Common.Models;

public record UserPaginationParams(
    int? PageNumber,
    int? PageSize,
    string? SearchString,
    Guid? UserId = null
) : PaginationParams(PageNumber, PageSize, SearchString);
