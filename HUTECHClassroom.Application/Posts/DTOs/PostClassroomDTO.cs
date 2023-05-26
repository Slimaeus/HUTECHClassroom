using HUTECHClassroom.Application.Classrooms.DTOs;
using HUTECHClassroom.Application.Common.DTOs;
using HUTECHClassroom.Domain.Enums;

namespace HUTECHClassroom.Application.Posts.DTOs;

public record PostClassroomDTO : BaseEntityDTO
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Room { get; set; }
    public string StudyPeriod { get; set; }
    public string Topic { get; set; }
    public string Class { get; set; }
    public string SchoolYear { get; set; }
    public string StudyGroup { get; set; }
    public string PracticalStudyGroup { get; set; }
    public Semester Semester { get; set; }
    public ClassroomType Type { get; set; }

    public ClassroomFacultyDTO Faculty { get; set; }
    public ClassroomSubjectDTO Subject { get; set; }


    public MemberDTO Lecturer { get; set; }
}
