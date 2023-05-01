namespace HUTECHClassroom.Domain.Entities;

public class GroupUser
{
    public Guid GroupId { get; set; }
    public virtual Group Group { get; set; }
    public Guid UserId { get; set; }
    public virtual ApplicationUser User { get; set; }
    public Guid GroupRoleId { get; set; }
    public virtual GroupRole GroupRole { get; set; }
}
