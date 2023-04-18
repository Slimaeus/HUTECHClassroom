using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Missions.DTOs;
using HUTECHClassroom.Domain.Entities;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Projects.Queries.GetProjectMission;

public record GetProjectMissionQuery(Guid Id, Guid MissionId) : GetQuery<MissionDTO>;
public class GetProjectMissionQueryHandler : GetQueryHandler<Mission, GetProjectMissionQuery, MissionDTO>
{
    public GetProjectMissionQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
    public override Expression<Func<Mission, bool>> FilterPredicate(GetProjectMissionQuery query)
    {
        return x => x.ProjectId == query.Id && x.Id == query.MissionId;
    }
    public override object GetNotFoundKey(GetProjectMissionQuery query)
    {
        return query.MissionId;
    }
}