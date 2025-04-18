using HUTECHClassroom.Application.Common.DTOs;

namespace HUTECHClassroom.Application.Majors.DTOs;

public record MajorFacultyDTO : BaseEntityDTO
{
    public string? Name { get; set; }
}