using System.ComponentModel.DataAnnotations;

namespace HUTECHClassroom.Web.ViewModels.ApplicationUsers;

public sealed class EditUserViewModel
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    [EmailAddress]
    public string Email { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public Guid FacultyId { get; set; }
    public string RoleName { get; set; } = string.Empty;
}
