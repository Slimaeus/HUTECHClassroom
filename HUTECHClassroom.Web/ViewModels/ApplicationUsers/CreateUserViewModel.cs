using System.ComponentModel.DataAnnotations;

namespace HUTECHClassroom.Web.ViewModels.ApplicationUsers;

public sealed class CreateUserViewModel
{
    public string UserName { get; set; } = string.Empty;
    [EmailAddress]
    public string? Email { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public Guid FacultyId { get; set; }
    public string? RoleName { get; set; }
}
