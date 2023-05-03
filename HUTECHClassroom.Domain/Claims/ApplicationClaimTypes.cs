namespace HUTECHClassroom.Domain.Claims;

public static class ApplicationClaimTypes
{
    public const string Mission = "mission";
    public const string Project = "project";
    public const string Group = "group";
    public const string Classroom = "classroom";
    public static readonly IList<string> EntityClaimTypes = new List<string>
    {
        Mission, Project, Group, Classroom
    };
}
