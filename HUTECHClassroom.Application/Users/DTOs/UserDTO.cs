using HUTECHClassroom.Application.Common.DTOs;
using HUTECHClassroom.Application.Faculties.DTOs;

namespace HUTECHClassroom.Application.Users.DTOs;

public record UserDTO(Guid Id, string UserName, string Email, string FirstName, string LastName, FacultyDTO Faculty) : IEntityDTO;