using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Domain.Enums;

namespace HUTECHClassroom.Application.Groups;

public record GroupPaginationParams(int? PageNumber, int? PageSize, string? SearchString, Guid? UserId) : UserPaginationParams(PageNumber, PageSize, SearchString, UserId)
{
    public SortingOrder NameOrder { get; set; }
    public SortingOrder DescriptionOrder { get; set; }
}
