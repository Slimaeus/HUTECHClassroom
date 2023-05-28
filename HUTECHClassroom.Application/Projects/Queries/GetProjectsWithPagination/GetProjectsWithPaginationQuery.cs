using EntityFrameworkCore.QueryBuilder.Interfaces;
using HUTECHClassroom.Application.Common.Extensions;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Projects.DTOs;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Projects.Queries.GetProjectsWithPagination;

public record GetProjectsWithPaginationQuery(ProjectPaginationParams Params) : GetWithPaginationQuery<ProjectDTO, ProjectPaginationParams>(Params);
public class GetProjectsWithPaginationQueryHandler : GetWithPaginationQueryHandler<Project, GetProjectsWithPaginationQuery, ProjectDTO, ProjectPaginationParams>
{
    public GetProjectsWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }
    protected override Expression<Func<Project, bool>> SearchStringPredicate(string searchString)
        => x => x.Name.ToLower().Contains(searchString.ToLower()) || x.Description.ToLower().Contains(searchString.ToLower());
    protected override IQuery<Project> Order(IMultipleResultQuery<Project> query) => query.OrderByDescending(x => x.CreateDate);
    protected override IMultipleResultQuery<Project> SortingQuery(IMultipleResultQuery<Project> query, GetProjectsWithPaginationQuery request)
        => query.SortEntityQuery(request.Params.NameOrder, x => x.Name)
                .SortEntityQuery(request.Params.DescriptionOrder, x => x.Description);
}

