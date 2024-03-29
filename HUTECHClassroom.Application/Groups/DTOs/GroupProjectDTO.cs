﻿using HUTECHClassroom.Application.Common.DTOs;

namespace HUTECHClassroom.Application.Groups.DTOs;

public sealed record GroupProjectDTO : BaseEntityDTO
{
    public string? Name { get; set; }
    public string? Description { get; set; }
}
