using HUTECHClassroom.Domain.Constants;

namespace HUTECHClassroom.Web.ViewModels.Faculties;

public sealed class ImportUsersToFacultyViewModel
{
    public Guid FacultyId { get; set; }
    public string FacultyName { get; set; }
    public string RoleName { get; set; } = RoleConstants.Student;
    public IFormFile File { get; set; }
}
