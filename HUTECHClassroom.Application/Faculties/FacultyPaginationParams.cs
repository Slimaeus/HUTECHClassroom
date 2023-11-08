using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Domain.Enums;

namespace HUTECHClassroom.Application.Faculties;

public record FacultyPaginationParams(int? PageNumber, int? PageSize, string? SearchString) : PaginationParams(PageNumber, PageSize, SearchString)
{
    public SortingOrder NameOrder { get; set; }
}
