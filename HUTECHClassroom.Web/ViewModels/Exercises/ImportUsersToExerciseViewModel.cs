namespace HUTECHClassroom.Web.ViewModels.Exercises;

public sealed class ImportUsersToExerciseViewModel
{
    public Guid ExerciseId { get; set; }
    public string? ExerciseTitle { get; set; }
    public IFormFile? File { get; set; }
}
