using AutoMapper;
using HUTECHClassroom.Application.Missions.DTOs;
using HUTECHClassroom.Domain.Entities;

namespace HUTECHClassroom.Application.Common.Mappings
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            #region Missions
            CreateMap<Mission, MissionDTO>();
            #endregion
        }
    }
}
