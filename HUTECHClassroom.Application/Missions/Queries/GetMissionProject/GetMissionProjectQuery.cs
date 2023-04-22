using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Projects.DTOs;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Missions.Queries.GetMissionProject;

public record GetMissionProjectQuery(Guid Id) : GetQuery<ProjectDTO>;
public class GetMissionProjectQueryHandler : GetQueryHandler<Project, GetMissionProjectQuery, ProjectDTO>
{
    public GetMissionProjectQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
    public override Expression<Func<Project, bool>> FilterPredicate(GetMissionProjectQuery query)
    {
        return x => x.Missions.Any(x => x.Id == query.Id);
    }
    public override object GetNotFoundKey(GetMissionProjectQuery query)
    {
        return query.Id;
    }
}
