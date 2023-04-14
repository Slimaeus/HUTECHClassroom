using Microsoft.AspNetCore.Identity;

namespace HUTECHClassroom.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<MissionUser> MissionUsers { get; set; }
    }
}
