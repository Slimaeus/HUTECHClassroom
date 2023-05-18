using HUTECHClassroom.Application.Common.DTOs;

namespace HUTECHClassroom.Application.Subjects.DTOs;

public record SubjectDTO : BaseEntityDTO<string>
{
    public string Title { get; set; }
    public int TotalCredits { get; set; }
}
