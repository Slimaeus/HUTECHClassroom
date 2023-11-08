namespace HUTECHClassroom.Web.ViewModels;

public sealed class ImportEntitiesFromExcelViewModel
{
    public IFormFile File { get; set; }
    public IEnumerable<string> PropertyNames { get; set; } = new HashSet<string>();
}
