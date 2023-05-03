using HUTECHClassroom.Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace HUTECHClassroom.Domain.Entities;

public class ApplicationUser : IdentityUser<Guid>, IEntity
{
    public Guid FacultyId { get; set; }
    public virtual Faculty Faculty { get; set; } = default!;
    public virtual ICollection<Group> Groups { get; set; } = new HashSet<Group>();
    public virtual ICollection<Answer> Answers { get; set; } = new HashSet<Answer>();
    public virtual ICollection<ExerciseUser> ExerciseUsers { get; set; } = new HashSet<ExerciseUser>();
    public virtual ICollection<ClassroomUser> ClassroomUsers { get; set; } = new HashSet<ClassroomUser>();
    public virtual ICollection<GroupUser> GroupUsers { get; set; } = new HashSet<GroupUser>();
    public virtual ICollection<MissionUser> MissionUsers { get; set; } = new HashSet<MissionUser>();
    public virtual ICollection<ApplicationUserRole> ApplicationUserRoles { get; set; } = new HashSet<ApplicationUserRole>();
}
