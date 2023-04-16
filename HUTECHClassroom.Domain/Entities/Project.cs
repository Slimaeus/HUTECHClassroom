using HUTECHClassroom.Domain.Common;

namespace HUTECHClassroom.Domain.Entities
{
    public class Project : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public Guid GroupId { get; set; }
        public virtual Group Group { get; set; }

        public virtual ICollection<Mission> Missions { get; set; } = new HashSet<Mission>();
    }
}
