using HUTECHClassroom.Application.Common.DTOs;

namespace HUTECHClassroom.Application.Projects.DTOs
{
    public record ProjectDTO : IEntityDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
