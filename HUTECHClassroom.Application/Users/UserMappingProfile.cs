using AutoMapper;
using HUTECHClassroom.Application.Users.DTOs;
using HUTECHClassroom.Domain.Entities;

namespace HUTECHClassroom.Application.Users;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<ApplicationUser, UserDTO>();
    }
}