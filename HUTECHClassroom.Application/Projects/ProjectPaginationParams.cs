using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Domain.Enums;

namespace HUTECHClassroom.Application.Projects;

public record ProjectPaginationParams(int? PageNumber, int? PageSize, string SearchString, Guid? UserId) : UserPaginationParams(PageNumber, PageSize, SearchString, UserId)
{
    public SortingOrder NameOrder { get; set; }
    public SortingOrder DescriptionOrder { get; set; }
}
