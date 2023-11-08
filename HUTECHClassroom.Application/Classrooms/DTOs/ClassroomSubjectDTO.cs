using HUTECHClassroom.Application.Common.DTOs;

namespace HUTECHClassroom.Application.Classrooms.DTOs;
public sealed record ClassroomSubjectDTO : BaseEntityDTO
{
    public string? Code { get; set; }
    public string? Title { get; set; }
    public int? TotalCredits { get; set; }
    public ClassroomSubjectMajorDTO? Major { get; set; }
}
