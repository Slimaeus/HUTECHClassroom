using HUTECHClassroom.Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace HUTECHClassroom.Domain.Entities;

public class ApplicationUser : IdentityUser<Guid>, IEntity
{
    [PersonalData]
    public string FirstName { get; set; } = string.Empty;
    [PersonalData]
    public string LastName { get; set; } = string.Empty;
    public Guid? FacultyId { get; set; }
    public virtual Faculty? Faculty { get; set; }
    public Guid? AvatarId { get; set; }
    public virtual Photo? Avatar { get; set; }
    public string? ClassId { get; set; }
    public virtual Class? Class { get; set; }
    public virtual ICollection<Classroom> Classrooms { get; set; } = new HashSet<Classroom>();
    public virtual ICollection<Group> Groups { get; set; } = new HashSet<Group>();
    public virtual ICollection<Post> Posts { get; set; } = new HashSet<Post>();
    public virtual ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
    public virtual ICollection<Answer> Answers { get; set; } = new HashSet<Answer>();
    public virtual ICollection<ExerciseUser> ExerciseUsers { get; set; } = new HashSet<ExerciseUser>();
    public virtual ICollection<ClassroomUser> ClassroomUsers { get; set; } = new HashSet<ClassroomUser>();
    public virtual ICollection<GroupUser> GroupUsers { get; set; } = new HashSet<GroupUser>();
    public virtual ICollection<MissionUser> MissionUsers { get; set; } = new HashSet<MissionUser>();
    public virtual ICollection<ApplicationUserRole> ApplicationUserRoles { get; set; } = new HashSet<ApplicationUserRole>();
}
