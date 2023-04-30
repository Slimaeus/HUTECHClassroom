using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Projects.DTOs;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Projects.Queries.GetProjectsWithPagination;

public record GetProjectsWithPaginationQuery(PaginationParams Params) : GetWithPaginationQuery<ProjectDTO, PaginationParams>(Params);
public class GetProjectsWithPaginationQueryHandler : GetWithPaginationQueryHandler<Project, GetProjectsWithPaginationQuery, ProjectDTO, PaginationParams>
{
    public GetProjectsWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }
    protected override Expression<Func<Project, bool>> SearchStringPredicate(string searchString) =>
        x => x.Name.ToLower().Contains(searchString.ToLower()) || x.Description.ToLower().Contains(searchString.ToLower());
    protected override Expression<Func<Project, object>> OrderByKeySelector()
    {
        return x => x.CreateDate;
    }
}

