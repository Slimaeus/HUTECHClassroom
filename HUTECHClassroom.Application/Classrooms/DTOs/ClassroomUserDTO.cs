using HUTECHClassroom.Application.Common.DTOs;
using HUTECHClassroom.Application.Groups.DTOs;

namespace HUTECHClassroom.Application.Classrooms.DTOs;

public sealed record ClassroomUserDTO(Guid Id, string? UserName, string? Email, string? FirstName, string? LastName, string? Class, IEnumerable<GroupDTO> Groups) : IEntityDTO;