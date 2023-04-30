namespace HUTECHClassroom.Web.ViewModels;

public class ImportEntitiesFromExcelViewModel
{
    public IFormFile File { get; set; }
    public IEnumerable<string> PropertyNames { get; set; } = new HashSet<string>();
}
