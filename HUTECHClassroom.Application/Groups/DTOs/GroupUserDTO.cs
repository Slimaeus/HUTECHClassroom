using HUTECHClassroom.Application.Common.DTOs;

namespace HUTECHClassroom.Application.Groups.DTOs;
public record GroupUserDTO(Guid Id, string UserName, string Email, string FirstName, string LastName, string GroupRole) : IEntityDTO;
