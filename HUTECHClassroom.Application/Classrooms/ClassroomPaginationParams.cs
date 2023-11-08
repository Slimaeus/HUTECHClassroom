using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Domain.Enums;

namespace HUTECHClassroom.Application.Classrooms;

public record ClassroomPaginationParams(Guid? UserId, int? PageNumber, int? PageSize, string? SearchString) : UserPaginationParams(PageNumber, PageSize, SearchString, UserId)
{
    public SortingOrder TitleOrder { get; set; }
    public SortingOrder DescriptionOrder { get; set; }
    public SortingOrder RoomOrder { get; set; }
    public SortingOrder TopicOrder { get; set; }
    public SortingOrder StudyPeriodOrder { get; set; }
    public SortingOrder ClassOrder { get; set; }
    public SortingOrder SchoolYearOrder { get; set; }
    public SortingOrder StudyGroupOrder { get; set; }
    public SortingOrder PracticalStudyGroupOrder { get; set; }
    public SortingOrder SemesterOrder { get; set; }
    public SortingOrder TypeOrder { get; set; }
}
