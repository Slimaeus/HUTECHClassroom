﻿using EntityFrameworkCore.QueryBuilder.Interfaces;
using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Projects.DTOs;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Groups.Queries.GetGroupProjectsWithPagination;

public record GetGroupProjectsWithPaginationQuery(Guid Id, PaginationParams Params) : GetWithPaginationQuery<ProjectDTO, PaginationParams>(Params);
public class GetGroupProjectsWithPaginationQueryHandler : GetWithPaginationQueryHandler<Project, GetGroupProjectsWithPaginationQuery, ProjectDTO, PaginationParams>
{
    public GetGroupProjectsWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
    protected override IQuery<Project> Order(IMultipleResultQuery<Project> query) => query.OrderByDescending(x => x.CreateDate);

    protected override Expression<Func<Project, bool>> SearchStringPredicate(string searchString)
    {
        var toLowerSearchString = searchString.ToLower();
        return x => x.Name.ToLower().Contains(toLowerSearchString) || x.Description.ToLower().Contains(searchString);
    }
    protected override Expression<Func<Project, bool>> FilterPredicate(GetGroupProjectsWithPaginationQuery query)
    {
        return x => x.GroupId == query.Id;
    }
}
