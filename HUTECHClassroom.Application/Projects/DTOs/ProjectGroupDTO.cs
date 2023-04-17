using HUTECHClassroom.Application.Common.DTOs;

namespace HUTECHClassroom.Application.Projects.DTOs
{
    public record ProjectGroupDTO : BaseEntityDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
