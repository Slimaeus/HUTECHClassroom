using HUTECHClassroom.Application.Common.DTOs;

namespace HUTECHClassroom.Application.Subjects.DTOs;

public sealed record SubjectDTO : BaseEntityDTO
{
    public string? Code { get; set; }
    public string? Title { get; set; }
    public int? TotalCredits { get; set; }

    public SubjectMajorDTO? Major { get; set; }
}
