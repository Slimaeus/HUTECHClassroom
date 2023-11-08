using HUTECHClassroom.Application.Common.DTOs;
using HUTECHClassroom.Domain.Enums;

namespace HUTECHClassroom.Application.Classrooms.DTOs;

public sealed record ClassroomDTO : BaseEntityDTO
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Room { get; set; } = string.Empty;
    public string StudyPeriod { get; set; } = string.Empty;
    public string Topic { get; set; } = string.Empty;
    public string? Class { get; set; } = string.Empty;
    public string SchoolYear { get; set; } = string.Empty;
    public string StudyGroup { get; set; } = string.Empty;
    public string PracticalStudyGroup { get; set; } = string.Empty;
    public Semester? Semester { get; set; }
    public ClassroomType? Type { get; set; }

    public ClassroomFacultyDTO? Faculty { get; set; }
    public ClassroomSubjectDTO? Subject { get; set; }


    public MemberDTO? Lecturer { get; set; }
}
