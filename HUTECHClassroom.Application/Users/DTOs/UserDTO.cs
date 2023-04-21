using HUTECHClassroom.Application.Common.DTOs;

namespace HUTECHClassroom.Application.Users.DTOs;

public record UserDTO(string UserName, string Email) : BaseEntityDTO;
