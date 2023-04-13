using HUTECHClassroom.Domain.Entities;

namespace HUTECHClassroom.Application.Missions.DTOs
{
    public class MissionDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsDone { get; set; } = false;
    }
}
