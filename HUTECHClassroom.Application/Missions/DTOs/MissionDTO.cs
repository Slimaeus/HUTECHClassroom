using HUTECHClassroom.Application.Common.DTOs;

namespace HUTECHClassroom.Application.Missions.DTOs
{
    public record MissionDTO : IEntityDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsDone { get; set; } = false;
    }
}
