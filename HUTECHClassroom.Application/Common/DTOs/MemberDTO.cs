namespace HUTECHClassroom.Application.Common.DTOs;

public record MemberDTO(string UserName, string Email, string FirstName, string LastName) : IEntityDTO
{
    public Guid Id { get; set; }
}
