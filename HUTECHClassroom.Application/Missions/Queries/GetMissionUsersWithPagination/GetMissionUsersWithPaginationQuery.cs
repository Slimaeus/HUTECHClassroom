using EntityFrameworkCore.QueryBuilder.Interfaces;
using HUTECHClassroom.Application.Common.DTOs;
using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Common.Requests;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Missions.Queries.GetMissionUsersWithPagination;

public sealed record GetMissionUsersWithPaginationQuery(Guid Id, PaginationParams Params) : GetWithPaginationQuery<MemberDTO, PaginationParams>(Params);
public sealed class GetMissionUsersWithPaginationQueryHandler : GetWithPaginationQueryHandler<ApplicationUser, GetMissionUsersWithPaginationQuery, MemberDTO, PaginationParams>
{
    public GetMissionUsersWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
    protected override Expression<Func<ApplicationUser, bool>> FilterPredicate(GetMissionUsersWithPaginationQuery query)
        => x => x.MissionUsers.Any(x => x.MissionId == query.Id);
    protected override Expression<Func<ApplicationUser, bool>> SearchStringPredicate(string searchString)
    {
        var toLowerSearchString = searchString.ToLower();
        return x => (x.UserName != null && x.UserName.ToLower().Contains(toLowerSearchString)) || (x.Email != null && x.Email.ToLower().Contains(toLowerSearchString));
    }
    protected override IQuery<ApplicationUser> Order(IMultipleResultQuery<ApplicationUser> query)
        => query.OrderBy(x => x.UserName);

}
