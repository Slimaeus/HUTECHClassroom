using HUTECHClassroom.Application.Common.DTOs;

namespace HUTECHClassroom.Application.Classrooms.DTOs;
public record ClassroomSubjectMajorDTO : BaseEntityDTO
{
    public string Code { get; set; }
    public string Title { get; set; }
    public int TotalCredits { get; set; }
    public int NonComulativeCredits { get; set; }
}
