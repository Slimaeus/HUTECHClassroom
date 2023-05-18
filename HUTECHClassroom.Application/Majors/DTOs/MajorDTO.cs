using HUTECHClassroom.Application.Common.DTOs;

namespace HUTECHClassroom.Application.Majors.DTOs;

public record MajorDTO : BaseEntityDTO<string>
{
    public string Title { get; set; }
    public int TotalCredits { get; set; }
    public int NonComulativeCredits { get; set; }
}
