using HUTECHClassroom.Application.Common.DTOs;

namespace HUTECHClassroom.Application.Classrooms.DTOs;

public record ClassroomFacultyDTO : BaseEntityDTO
{
    public string? Name { get; set; }
}