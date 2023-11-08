namespace HUTECHClassroom.Web.ViewModels.Majors;

public sealed class ImportSubjectsToMajorViewModel
{
    public Guid MajorId { get; set; }
    public string? MajorTitle { get; set; }
    public IFormFile? File { get; set; }
}
