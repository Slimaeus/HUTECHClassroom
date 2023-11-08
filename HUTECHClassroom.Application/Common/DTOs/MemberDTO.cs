namespace HUTECHClassroom.Application.Common.DTOs;

public sealed record MemberDTO(Guid Id, string? UserName, string? Email, string? FirstName, string? LastName, string? Class, string? AvatarUrl) : IEntityDTO;