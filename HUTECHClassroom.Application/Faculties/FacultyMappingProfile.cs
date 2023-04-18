using AutoMapper;
using HUTECHClassroom.Application.Faculties.Commands.CreateFaculty;
using HUTECHClassroom.Application.Faculties.Commands.UpdateFaculty;
using HUTECHClassroom.Application.Faculties.DTOs;
using HUTECHClassroom.Domain.Entities;

namespace HUTECHClassroom.Application.Faculties;

public class FacultyMappingProfile : Profile
{
    public FacultyMappingProfile()
    {
        CreateMap<Faculty, FacultyDTO>();
        CreateMap<CreateFacultyCommand, Faculty>();
        CreateMap<UpdateFacultyCommand, Faculty>()
            .ForAllMembers(options => options.Condition((src, des, srcValue, desValue) => srcValue != null));
    }
}
