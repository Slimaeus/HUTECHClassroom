namespace HUTECHClassroom.Web.ViewModels.Groups;

public class ImportUsersToGroupViewModel
{
    public Guid GroupId { get; set; }
    public string GroupName { get; set; }
    public IFormFile File { get; set; }
}
