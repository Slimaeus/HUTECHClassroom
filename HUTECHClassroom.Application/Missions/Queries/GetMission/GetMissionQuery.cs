using HUTECHClassroom.Application.Common.Interfaces;
using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Missions.DTOs;
using HUTECHClassroom.Domain.Interfaces;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Missions.Queries.GetMission;

public record GetMissionQuery(Guid Id) : GetQuery<MissionDTO>;
public class GetMissionQueryHandler : GetQueryHandler<Mission, GetMissionQuery, MissionDTO>
{
    private readonly IUserAccessor _userAccessor;

    public GetMissionQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserAccessor userAccessor) : base(unitOfWork, mapper)
    {
        _userAccessor = userAccessor;
    }
    public override Expression<Func<Mission, bool>> FilterPredicate(GetMissionQuery query)
    {
        return x => x.Id == query.Id;
    }
    protected override IMappingParams GetMappingParameters()
    {
        return new UserMappingParams { UserId = _userAccessor.Id };
    }
    public override object GetNotFoundKey(GetMissionQuery query) => query.Id;
}
