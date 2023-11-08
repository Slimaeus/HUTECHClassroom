namespace HUTECHClassroom.Web.ViewModels.Missions;

public sealed class ImportUsersToMissionViewModel
{
    public Guid MissionId { get; set; }
    public string MissionTitle { get; set; }
    public IFormFile File { get; set; }
}
