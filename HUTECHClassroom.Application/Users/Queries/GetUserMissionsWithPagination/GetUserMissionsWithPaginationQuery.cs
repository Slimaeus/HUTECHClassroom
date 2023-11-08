using EntityFrameworkCore.QueryBuilder.Interfaces;
using HUTECHClassroom.Application.Common.Interfaces;
using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Missions.DTOs;
using HUTECHClassroom.Domain.Interfaces;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Users.Queries.GetUserMissionsWithPagination;

public sealed record GetUserMissionsWithPaginationQuery(PaginationParams Params) : GetWithPaginationQuery<MissionDTO, PaginationParams>(Params);
public sealed class GetUserMissionsWithPaginationQueryHandler : GetWithPaginationQueryHandler<Mission, GetUserMissionsWithPaginationQuery, MissionDTO, PaginationParams>
{
    private readonly IUserAccessor _userAccessor;

    public GetUserMissionsWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserAccessor userAccessor) : base(unitOfWork, mapper)
        => _userAccessor = userAccessor;
    protected override IMappingParams GetMappingParameters()
        => new UserMappingParams { UserId = _userAccessor.Id };
    protected override IQuery<Mission> Order(IMultipleResultQuery<Mission> query)
        => query.OrderByDescending(x => x.CreateDate);
    protected override Expression<Func<Mission, bool>> FilterPredicate(GetUserMissionsWithPaginationQuery query)
        => x => x.MissionUsers.Any(y => y.UserId == _userAccessor.Id);
}
