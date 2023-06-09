﻿using HUTECHClassroom.Application.Account.DTOs;
using HUTECHClassroom.Application.Common.DTOs;

namespace HUTECHClassroom.Application.Common.Mappings;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<ApplicationUser, MemberDTO>();

        CreateMap<Faculty, UserFacultyDTO>();
    }
}
