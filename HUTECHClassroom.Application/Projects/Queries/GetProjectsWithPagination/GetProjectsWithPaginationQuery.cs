using EntityFrameworkCore.QueryBuilder.Interfaces;
using HUTECHClassroom.Application.Common.Extensions;
using HUTECHClassroom.Application.Common.Interfaces;
using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Projects.DTOs;
using HUTECHClassroom.Domain.Interfaces;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Projects.Queries.GetProjectsWithPagination;

public sealed record GetProjectsWithPaginationQuery(ProjectPaginationParams Params) : GetWithPaginationQuery<ProjectDTO, ProjectPaginationParams>(Params);
public sealed class GetProjectsWithPaginationQueryHandler : GetWithPaginationQueryHandler<Project, GetProjectsWithPaginationQuery, ProjectDTO, ProjectPaginationParams>
{
    private readonly IUserAccessor _userAccessor;

    public GetProjectsWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserAccessor userAccessor) : base(unitOfWork, mapper)
        => _userAccessor = userAccessor;
    protected override Expression<Func<Project, bool>> FilterPredicate(GetProjectsWithPaginationQuery query)
    => x => query.Params.UserId == null || query.Params.UserId == Guid.Empty || (x.Group != null && query.Params.UserId == x.Group.LeaderId) || x.Missions.Any(m => m.MissionUsers.Any(mu => query.Params.UserId == mu.UserId));
    protected override IMappingParams GetMappingParameters()
    => new UserMappingParams { UserId = _userAccessor.Id };
    protected override Expression<Func<Project, bool>> SearchStringPredicate(string searchString)
        => x => x.Name.ToLower().Contains(searchString.ToLower()) || (x.Description != null && x.Description.ToLower().Contains(searchString.ToLower()));
    protected override IQuery<Project> Order(IMultipleResultQuery<Project> query) => query.OrderByDescending(x => x.CreateDate);
    protected override IMultipleResultQuery<Project> SortingQuery(IMultipleResultQuery<Project> query, GetProjectsWithPaginationQuery request)
        => query.SortEntityQuery(request.Params.NameOrder, x => x.Name)
                .SortEntityQuery(request.Params.DescriptionOrder, x => x.Description);
}

