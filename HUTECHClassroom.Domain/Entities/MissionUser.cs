namespace HUTECHClassroom.Domain.Entities;

public class MissionUser
{
    public Guid MissionId { get; set; }
    public virtual Mission Mission { get; set; }
    public Guid UserId { get; set; }
    public virtual ApplicationUser User { get; set; }
}
