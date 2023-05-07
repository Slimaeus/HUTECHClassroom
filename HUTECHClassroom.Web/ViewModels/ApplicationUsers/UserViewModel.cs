using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HUTECHClassroom.Web.ViewModels.ApplicationUsers;

public record UserViewModel
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    [EmailAddress]
    public string Email { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    [DisplayName("Faculty")]
    public String FacultyName { get; set; }
}
