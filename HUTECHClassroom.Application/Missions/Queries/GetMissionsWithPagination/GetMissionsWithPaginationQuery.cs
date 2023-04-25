using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Missions.DTOs;
using HUTECHClassroom.Infrastructure.Services;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Missions.Queries.GetMissionsWithPagination;

public record GetMissionsWithPaginationQuery(PaginationParams Params) : GetWithPaginationQuery<MissionDTO>(Params);
public class GetMissionsWithPaginationQueryHandler : GetWithPaginationQueryHandler<Mission, GetMissionsWithPaginationQuery, MissionDTO>
{
    private readonly IUserAccessor _userAccessor;

    public GetMissionsWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserAccessor userAccessor) : base(unitOfWork, mapper)
    {
        _userAccessor = userAccessor;
    }
    protected override Expression<Func<Mission, bool>> FilterPredicate(GetMissionsWithPaginationQuery query)
    {
        return x => x.MissionUsers.Any(mu => mu.UserId == _userAccessor.Id);
    }
    protected override Expression<Func<Mission, bool>> SearchStringPredicate(string searchString)
        => x => x.Title.ToLower().Contains(searchString.ToLower()) || x.Description.ToLower().Contains(searchString.ToLower());
    protected override Expression<Func<Mission, object>> OrderByKeySelector()
    {
        return x => x.CreateDate;
    }
}

