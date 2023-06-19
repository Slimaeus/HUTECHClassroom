namespace HUTECHClassroom.Web.ViewModels.Faculties;

public class ImportClassroomsToFacultyViewModel
{
    public Guid FacultyId { get; set; }
    public string FacultyName { get; set; }
    public IFormFile File { get; set; }
}
