using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Missions.DTOs;
using HUTECHClassroom.Domain.Entities;

namespace HUTECHClassroom.Application.Missions.Queries.GetMission
{
    public record GetMissionQuery(Guid Id) : GetQuery<MissionDTO>(Id);
    public class GetMissionQueryHandler : GetQueryHandler<Mission, GetMissionQuery, MissionDTO>
    {
        public GetMissionQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }
    }
}
