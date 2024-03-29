﻿using EntityFrameworkCore.QueryBuilder.Interfaces;
using HUTECHClassroom.Application.Common.Interfaces;
using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Groups.DTOs;
using HUTECHClassroom.Domain.Interfaces;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Users.Queries.GetUserGroupsWithPagination;

public sealed record GetUserGroupsWithPaginationQuery(PaginationParams Params) : GetWithPaginationQuery<GroupDTO, PaginationParams>(Params);
public sealed class GetUserGroupsWithPaginationQueryHandler : GetWithPaginationQueryHandler<Group, GetUserGroupsWithPaginationQuery, GroupDTO, PaginationParams>
{
    private readonly IUserAccessor _userAccessor;

    public GetUserGroupsWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserAccessor userAccessor) : base(unitOfWork, mapper)
        => _userAccessor = userAccessor;
    protected override IMappingParams GetMappingParameters()
        => new UserMappingParams { UserId = _userAccessor.Id };
    protected override IQuery<Group> Order(IMultipleResultQuery<Group> query)
        => query.OrderByDescending(x => x.CreateDate);
    protected override Expression<Func<Group, bool>> FilterPredicate(GetUserGroupsWithPaginationQuery query)
        => x => x.GroupUsers.Any(y => y.UserId == _userAccessor.Id) || x.LeaderId == _userAccessor.Id;
}
