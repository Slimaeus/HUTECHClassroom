using AutoMapper;
using HUTECHClassroom.Application.Classrooms.DTOs;
using HUTECHClassroom.Domain.Entities;

namespace HUTECHClassroom.Application.Classrooms
{
    public class ClassroomMappingProfile : Profile
    {
        public ClassroomMappingProfile()
        {
            CreateMap<Classroom, ClassroomDTO>();
        }
    }
}
