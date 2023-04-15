using HUTECHClassroom.Domain.Common;

namespace HUTECHClassroom.Domain.Entities
{
    public class Project : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Mission> Missions { get; set; }
    }
}
