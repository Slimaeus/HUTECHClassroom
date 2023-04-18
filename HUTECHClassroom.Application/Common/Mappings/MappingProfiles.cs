using AutoMapper;
using HUTECHClassroom.Application.Common.DTOs;
using HUTECHClassroom.Domain.Entities;

namespace HUTECHClassroom.Application.Common.Mappings;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        #region User
        CreateMap<ApplicationUser, MemberDTO>();
        #endregion
    }
}
