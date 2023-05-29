using EntityFrameworkCore.QueryBuilder.Interfaces;
using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Projects.DTOs;
using HUTECHClassroom.Domain.Interfaces;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Users.Queries.GetUserProjectsWithPagination;

public record GetUserProjectsWithPaginationQuery(PaginationParams Params) : GetWithPaginationQuery<ProjectDTO, PaginationParams>(Params);
public class GetUserProjectsWithPaginationQueryHandler : GetWithPaginationQueryHandler<Project, GetUserProjectsWithPaginationQuery, ProjectDTO, PaginationParams>
{
    private readonly IUserAccessor _userAccessor;

    public GetUserProjectsWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserAccessor userAccessor) : base(unitOfWork, mapper)
    {
        _userAccessor = userAccessor;
    }
    protected override IQuery<Project> Order(IMultipleResultQuery<Project> query) => query.OrderByDescending(x => x.CreateDate);
    protected override Expression<Func<Project, bool>> FilterPredicate(GetUserProjectsWithPaginationQuery query)
        => x => x.Group.GroupUsers.Any(y => y.UserId == _userAccessor.Id) || x.Group.LeaderId == _userAccessor.Id;
}
