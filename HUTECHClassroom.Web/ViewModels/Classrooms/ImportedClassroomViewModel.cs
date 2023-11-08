using HUTECHClassroom.Domain.Enums;

namespace HUTECHClassroom.Web.ViewModels.Classrooms;

public sealed class ImportedClassroomViewModel
{
    public string Title { get; set; } = string.Empty;
    public string? Topic { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Room { get; set; } = string.Empty;
    public string? StudyPeriod { get; set; }
    public string? Class { get; set; }
    public string SchoolYear { get; set; } = string.Empty;
    public string? StudyGroup { get; set; }
    public string? PracticalStudyGroup { get; set; }
    public Semester Semester { get; set; } = Semester.I;
    public ClassroomType Type { get; set; } = ClassroomType.TheoryRoom;
    public string? LecturerName { get; set; }
    public string? SubjectCode { get; set; }
}
