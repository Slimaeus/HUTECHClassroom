using HUTECHClassroom.Application.Common.DTOs;

namespace HUTECHClassroom.Application.Faculties.DTOs;

public sealed record FacultyDTO : BaseEntityDTO
{
    public string? Name { get; set; }
}
