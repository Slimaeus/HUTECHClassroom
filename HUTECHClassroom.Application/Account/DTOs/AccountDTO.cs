using HUTECHClassroom.Application.Common.DTOs;

namespace HUTECHClassroom.Application.Account.DTOs;

public record AccountDTO : IEntityDTO
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public UserFacultyDTO Faculty { get; set; }
    public IEnumerable<string> Roles { get; set; }
    public string Token { get; set; }
}
