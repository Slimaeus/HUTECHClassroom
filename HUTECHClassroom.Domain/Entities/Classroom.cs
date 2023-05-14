using HUTECHClassroom.Domain.Common;

namespace HUTECHClassroom.Domain.Entities;

public class Classroom : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Topic { get; set; } = string.Empty;
    public string Room { get; set; } = string.Empty;
    public string? Description { get; set; }
    //public string? Class { get; set; }
    //public Semester Semester { get; set; } = Semester.I;
    //public string SchoolYear { get; set; } = string.Empty;
    //public string? StudyGroup { get; set; }
    //public string? PracticalStudyGroup { get; set; }

    public Guid LecturerId { get; set; }
    public virtual ApplicationUser? Lecturer { get; set; }
    public Guid FacultyId { get; set; }
    public virtual Faculty? Faculty { get; set; }
    //public Guid SubjectId { get; set; }
    //public virtual Subject? Subject { get; set; }

    public virtual ICollection<Group> Groups { get; set; } = new HashSet<Group>();
    public virtual ICollection<Post> Posts { get; set; } = new HashSet<Post>();
    public virtual ICollection<ClassroomUser> ClassroomUsers { get; set; } = new HashSet<ClassroomUser>();
    public virtual ICollection<Exercise> Excercises { get; set; } = new HashSet<Exercise>();
}
