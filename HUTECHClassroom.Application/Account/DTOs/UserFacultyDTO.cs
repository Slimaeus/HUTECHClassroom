using HUTECHClassroom.Application.Common.DTOs;

namespace HUTECHClassroom.Application.Account.DTOs;

public record UserFacultyDTO : BaseEntityDTO
{
    public string Name { get; set; }
}
