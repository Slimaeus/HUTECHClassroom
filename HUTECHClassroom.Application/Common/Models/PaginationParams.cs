namespace HUTECHClassroom.Application.Common.Models;

public record PaginationParams(
    int? PageNumber = 1,
    int? PageSize = 10,
    string SearchString = null);
