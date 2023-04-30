using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Domain.Enums;

namespace HUTECHClassroom.Application.Classrooms;

public record ClassroomPaginationParams(int? PageNumber, int? PageSize, string SearchString) : PaginationParams(PageNumber, PageSize, SearchString)
{
    public SortingOrder TitleOrder { get; set; }
    public SortingOrder DescriptionOrder { get; set; }
    public SortingOrder RoomOrder { get; set; }
    public SortingOrder TopicOrder { get; set; }
}
