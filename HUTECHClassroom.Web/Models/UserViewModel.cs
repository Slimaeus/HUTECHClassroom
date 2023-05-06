namespace HUTECHClassroom.Web.Models;

public class UserViewModel
{
    public string UserName { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public Guid FacultyId { get; set; }
}
