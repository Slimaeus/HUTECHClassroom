using EntityFrameworkCore.QueryBuilder.Interfaces;
using HUTECHClassroom.Application.Common.Interfaces;
using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Groups.DTOs;
using HUTECHClassroom.Domain.Interfaces;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Classrooms.Queries.GetClassroomGroupsWithPagination;

public sealed record GetClassroomGroupsWithPaginationQuery(Guid Id, ClassroomPaginationParams Params) : GetWithPaginationQuery<GroupDTO, ClassroomPaginationParams>(Params);
public sealed class GetClassroomGroupsWithPaginationQueryHandler : GetWithPaginationQueryHandler<Group, GetClassroomGroupsWithPaginationQuery, GroupDTO, ClassroomPaginationParams>
{
    private readonly IUserAccessor _userAccessor;

    public GetClassroomGroupsWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserAccessor userAccessor) : base(unitOfWork, mapper)
        => _userAccessor = userAccessor;
    protected override Expression<Func<Group, bool>> FilterPredicate(GetClassroomGroupsWithPaginationQuery query)
        => x => x.ClassroomId == query.Id && (query.Params.UserId == null || query.Params.UserId == Guid.Empty || x.GroupUsers.Any(gu => query.Params.UserId == gu.UserId));
    protected override IMappingParams GetMappingParameters()
        => new UserMappingParams { UserId = _userAccessor.Id };
    protected override Expression<Func<Group, bool>> SearchStringPredicate(string searchString)
    {
        var toLowerSearchString = searchString.ToLower();
        return x => x.Name.ToLower().Contains(toLowerSearchString) || (x.Description != null && x.Description.ToLower().Contains(toLowerSearchString));
    }
    protected override IQuery<Group> Order(IMultipleResultQuery<Group> query)
        => query.OrderByDescending(x => x.CreateDate);

}
