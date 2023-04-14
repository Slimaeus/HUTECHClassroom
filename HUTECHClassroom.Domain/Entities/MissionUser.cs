namespace HUTECHClassroom.Domain.Entities
{
    public class MissionUser
    {
        public Guid MissionId { get; set; }
        public Mission Mission { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
