using HUTECHClassroom.Application.Common.DTOs;

namespace HUTECHClassroom.Application.Account.DTOs;

public record AccountDTO : IEntityDTO
{
    public Guid Id { get; init; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? AvatarUrl { get; set; }
    public UserFacultyDTO? Faculty { get; set; }
    public string? Class { get; set; }
    public IEnumerable<string> Roles { get; set; } = Enumerable.Empty<string>();
    public string? Token { get; set; }
}
