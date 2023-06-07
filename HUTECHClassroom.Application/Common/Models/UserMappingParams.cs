using HUTECHClassroom.Application.Common.Interfaces;

namespace HUTECHClassroom.Application.Common.Models;

public class UserMappingParams : IMappingParams
{
    public Guid UserId { get; set; }
}
