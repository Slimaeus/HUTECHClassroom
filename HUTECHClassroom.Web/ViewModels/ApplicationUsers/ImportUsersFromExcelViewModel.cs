namespace HUTECHClassroom.Web.ViewModels.ApplicationUsers;

public class ImportUsersFromExcelViewModel
{
    public IFormFile File { get; set; }
    public IEnumerable<string> PropertyNames { get; set; } = new HashSet<string>();

}
