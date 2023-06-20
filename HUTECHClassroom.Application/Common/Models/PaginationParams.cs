namespace HUTECHClassroom.Application.Common.Models;

public record PaginationParams(
    int? PageNumber = 1,
    int? PageSize = 1000,
    string SearchString = null);
