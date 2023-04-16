﻿using HUTECHClassroom.Application.Common.DTOs;

namespace HUTECHClassroom.Application.Projects.DTOs
{
    public record ProjectDTO : BaseEntityDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
