using EntityFrameworkCore.QueryBuilder.Interfaces;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Projects.DTOs;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Groups.Queries.GetGroupProjectsWithPagination;

public record GetGroupProjectsWithPaginationQuery(Guid Id, GroupPaginationParams Params) : GetWithPaginationQuery<ProjectDTO, GroupPaginationParams>(Params);
public class GetGroupProjectsWithPaginationQueryHandler : GetWithPaginationQueryHandler<Project, GetGroupProjectsWithPaginationQuery, ProjectDTO, GroupPaginationParams>
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
        return x => x.GroupId == query.Id && (query.Params.UserId == null || query.Params.UserId == Guid.Empty || query.Params.UserId == x.Group.LeaderId || x.Group.GroupUsers.Any(gu => query.Params.UserId == gu.UserId));
    }
}
