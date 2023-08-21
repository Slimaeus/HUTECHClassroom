namespace HUTECHClassroom.Application.Common.DTOs;

public record MemberDTO(Guid Id, string UserName, string Email, string FirstName, string LastName, string AvatarUrl) : IEntityDTO
{ }
