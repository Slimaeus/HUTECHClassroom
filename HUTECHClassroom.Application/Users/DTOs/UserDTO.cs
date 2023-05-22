using HUTECHClassroom.Application.Common.DTOs;
using HUTECHClassroom.Application.Faculties.DTOs;

namespace HUTECHClassroom.Application.Users.DTOs;

public record UserDTO(string UserName, string Email, string FirstName, string LastName) : IEntityDTO
{
    public Guid Id { get; set; }
    public FacultyDTO Faculty { get; set; }
};
