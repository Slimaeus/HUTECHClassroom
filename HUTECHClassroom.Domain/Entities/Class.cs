using HUTECHClassroom.Domain.Common;

namespace HUTECHClassroom.Domain.Entities;

public class Class : BaseEntity<string>
{
    public string Name => Id;
    public ICollection<ApplicationUser> Users { get; set; } = new HashSet<ApplicationUser>();
}
