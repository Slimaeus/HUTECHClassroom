﻿using EntityFrameworkCore.QueryBuilder.Interfaces;
using HUTECHClassroom.Application.Common.Interfaces;
using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Projects.DTOs;
using HUTECHClassroom.Domain.Interfaces;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Users.Queries.GetUserProjectsWithPagination;

public sealed record GetUserProjectsWithPaginationQuery(PaginationParams Params) : GetWithPaginationQuery<ProjectDTO, PaginationParams>(Params);
public sealed class GetUserProjectsWithPaginationQueryHandler : GetWithPaginationQueryHandler<Project, GetUserProjectsWithPaginationQuery, ProjectDTO, PaginationParams>
{
    private readonly IUserAccessor _userAccessor;

    public GetUserProjectsWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserAccessor userAccessor) : base(unitOfWork, mapper)
        => _userAccessor = userAccessor;
    protected override IMappingParams GetMappingParameters()
        => new UserMappingParams { UserId = _userAccessor.Id };
    protected override IQuery<Project> Order(IMultipleResultQuery<Project> query)
        => query.OrderByDescending(x => x.CreateDate);
    protected override Expression<Func<Project, bool>> FilterPredicate(GetUserProjectsWithPaginationQuery query)
        => x => x.Group != null && (x.Group.GroupUsers.Any(y => y.UserId == _userAccessor.Id) || x.Group.LeaderId == _userAccessor.Id);
}
