using HUTECHClassroom.Domain.Common;

namespace HUTECHClassroom.Domain.Entities
{
    public class Group : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public Guid LecturerId { get; set; }
        public virtual ApplicationUser Lecturer { get; set; }

        public virtual ICollection<Project> Projects { get; set; } = new HashSet<Project>();
    }
}
