using EntityFrameworkCore.QueryBuilder.Interfaces;
using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Users.DTOs;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Users.Queries.GetUsersByIds;

public sealed record GetUsersByIdsQuery(IList<Guid> UserIds, PaginationParams Params) : GetWithPaginationQuery<UserDTO, PaginationParams>(Params);
public sealed class Handler : GetWithPaginationQueryHandler<ApplicationUser, GetUsersByIdsQuery, UserDTO, PaginationParams>
{
    public Handler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    protected override Expression<Func<ApplicationUser, bool>> FilterPredicate(GetUsersByIdsQuery query)
        => x => query.UserIds.Any(id => id == x.Id);

    protected override IQuery<ApplicationUser> Order(IMultipleResultQuery<ApplicationUser> query)
        => query.OrderBy(x => x.UserName);

    protected override Expression<Func<ApplicationUser, bool>> SearchStringPredicate(string searchString)
    {
        var toLowerSearchString = searchString.ToLower();
        return x => x.UserName != null && x.UserName.ToLower().Contains(toLowerSearchString) || x.Email != null && x.Email.ToLower().Contains(toLowerSearchString);
    }
}
