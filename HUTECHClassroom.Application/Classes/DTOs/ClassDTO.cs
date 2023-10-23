using HUTECHClassroom.Application.Common.DTOs;

namespace HUTECHClassroom.Application.Classes.DTOs;

public sealed record ClassDTO(string Id, string Name) : BaseEntityDTO<string>(Id);
