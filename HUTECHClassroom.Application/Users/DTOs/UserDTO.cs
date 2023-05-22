using HUTECHClassroom.Application.Common.DTOs;

namespace HUTECHClassroom.Application.Users.DTOs;

public record UserDTO(string UserName, string Email, string FirstName, string LastName) : IEntityDTO
{
    public Guid Id { get; set; }
};
