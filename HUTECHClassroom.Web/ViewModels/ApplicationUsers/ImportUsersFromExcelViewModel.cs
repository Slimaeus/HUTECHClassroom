using HUTECHClassroom.Domain.Constants;

namespace HUTECHClassroom.Web.ViewModels.ApplicationUsers;

public sealed class ImportUsersFromExcelViewModel
{
    public IFormFile? File { get; set; }
    public string RoleName { get; set; } = RoleConstants.Student;
    public IEnumerable<string> PropertyNames { get; set; } = new HashSet<string>();

}
