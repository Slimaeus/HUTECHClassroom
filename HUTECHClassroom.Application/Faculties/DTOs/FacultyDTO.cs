using HUTECHClassroom.Application.Common.DTOs;

namespace HUTECHClassroom.Application.Faculties.DTOs;

public record FacultyDTO : BaseEntityDTO
{
    public string Name { get; set; }
}
