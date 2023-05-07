namespace HUTECHClassroom.Web.ViewModels;

public class ImportUsersToClassroomViewModel
{
    public Guid ClassroomId { get; set; }
    public string ClassroomTitle { get; set; }
    public IFormFile File { get; set; }
}
