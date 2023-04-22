using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Groups.DTOs;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Groups.Queries.GetGroupProjectsWithPagination;

public record GetGroupProjectsWithPaginationQuery(Guid Id, PaginationParams Params) : GetWithPaginationQuery<GroupProjectDTO>(Params);
public class GetGroupProjectsWithPaginationQueryHandler : GetWithPaginationQueryHandler<Project, GetGroupProjectsWithPaginationQuery, GroupProjectDTO>
{
    public GetGroupProjectsWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
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
