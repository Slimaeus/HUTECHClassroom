using HUTECHClassroom.Domain.Common;

namespace HUTECHClassroom.Domain.Entities
{
    public class Mission : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsDone { get; set; } = false;

        public virtual ICollection<MissionUser> MissionUsers { get; set; }
    }
}
