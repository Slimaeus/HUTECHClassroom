using EntityFrameworkCore.QueryBuilder.Interfaces;
using HUTECHClassroom.Application.Common.Extensions;
using HUTECHClassroom.Application.Common.Interfaces;
using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Groups.DTOs;
using HUTECHClassroom.Domain.Interfaces;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Groups.Queries.GetGroupsWithPagination;

public record GetGroupsWithPaginationQuery(GroupPaginationParams Params) : GetWithPaginationQuery<GroupDTO, GroupPaginationParams>(Params);
public class GetGroupsWithPaginationQueryHandler : GetWithPaginationQueryHandler<Group, GetGroupsWithPaginationQuery, GroupDTO, GroupPaginationParams>
{
    private readonly IUserAccessor _userAccessor;

    public GetGroupsWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserAccessor userAccessor) : base(unitOfWork, mapper)
    {
        _userAccessor = userAccessor;
    }
    protected override IMappingParams GetMappingParameters()
    {
        return new UserMappingParams { UserId = _userAccessor.Id };
    }
    protected override Expression<Func<Group, bool>> SearchStringPredicate(string searchString)
    {
        return x => x.Name.ToLower().Equals(searchString.ToLower()) || x.Description.ToLower().Equals(searchString.ToLower());
    }
    protected override IQuery<Group> Order(IMultipleResultQuery<Group> query) => query.OrderByDescending(x => x.CreateDate);

    protected override IMultipleResultQuery<Group> SortingQuery(IMultipleResultQuery<Group> query, GetGroupsWithPaginationQuery request)
        => query.SortEntityQuery(request.Params.NameOrder, x => x.Name)
                .SortEntityQuery(request.Params.DescriptionOrder, x => x.Description);
}
