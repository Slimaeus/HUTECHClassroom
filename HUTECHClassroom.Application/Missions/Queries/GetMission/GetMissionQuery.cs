using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Missions.DTOs;
using HUTECHClassroom.Domain.Entities;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Missions.Queries.GetMission
{
    public record GetMissionQuery(Guid Id) : GetQuery<MissionDTO>;
    public class GetMissionQueryHandler : GetQueryHandler<Mission, GetMissionQuery, MissionDTO>
    {
        public GetMissionQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }
        public override Expression<Func<Mission, bool>> FilterPredicate(GetMissionQuery query)
        {
            return x => x.Id == query.Id;
        }
        public override object GetNotFoundKey(GetMissionQuery query) => query.Id;
    }
}
