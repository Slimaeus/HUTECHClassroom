using HUTECHClassroom.Application.Common.DTOs;

namespace HUTECHClassroom.Application.Classrooms.DTOs;
public record ClassroomSubjectDTO : BaseEntityDTO<string>
{
    public string Title { get; set; }
    public int TotalCredits { get; set; }
    public ClassroomSubjectMajorDTO Major { get; set; }
}
