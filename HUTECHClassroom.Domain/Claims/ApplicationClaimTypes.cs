namespace HUTECHClassroom.Domain.Claims;

public static class ApplicationClaimTypes
{
    public const string MISSION = "mission";
    public const string PROJECT = "project";
    public const string GROUP = "group";
    public const string CLASSROOM = "classroom";
    public const string EXERCISE = "exercise";
    public const string ANSWER = "answer";
    public const string POST = "post";
    public const string COMMENT = "comment";
    public const string USER = "user";
    public const string FACULTY = "faculty";
    public const string MAJOR = "major";
    public const string SUBJECT = "subject";
    public static readonly List<string> EntityClaimTypes =
    [
        MISSION, PROJECT, GROUP, CLASSROOM, EXERCISE, ANSWER, POST, COMMENT, USER, FACULTY, MAJOR, SUBJECT
    ];
}
