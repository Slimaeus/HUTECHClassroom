using EntityFrameworkCore.QueryBuilder.Interfaces;
using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Groups.DTOs;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Groups.Queries.GetGroupUsersWithPagination;

public sealed record GetGroupUsersWithPaginationQuery(Guid Id, PaginationParams Params) : GetWithPaginationQuery<GroupUserDTO, PaginationParams>(Params);
public sealed class GetGroupUsersWithPaginationQueryHandler : GetWithPaginationQueryHandler<GroupUser, GetGroupUsersWithPaginationQuery, GroupUserDTO, PaginationParams>
{
    public GetGroupUsersWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
    protected override Expression<Func<GroupUser, bool>> FilterPredicate(GetGroupUsersWithPaginationQuery query)
        => x => x.GroupId == query.Id;
    protected override Expression<Func<GroupUser, bool>> SearchStringPredicate(string searchString)
    {
        var toLowerSearchString = searchString.ToLower();
        return x => x.User != null && (x.User.UserName != null && x.User.UserName.ToLower().Contains(toLowerSearchString) || x.User.Email != null && x.User.Email.ToLower().Contains(toLowerSearchString));
    }
    protected override IQuery<GroupUser> Order(IMultipleResultQuery<GroupUser> query)
        => query.OrderBy(x => x.User!.FirstName);

}
