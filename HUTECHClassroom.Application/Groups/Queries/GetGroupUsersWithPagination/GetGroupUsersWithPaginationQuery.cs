using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using HUTECHClassroom.Application.Common.DTOs;
using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Domain.Entities;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Groups.Queries.GetGroupUsersWithPagination;

public record GetGroupUsersWithPaginationQuery(Guid Id, PaginationParams Params) : GetWithPaginationQuery<MemberDTO>(Params);
public class GetGroupUsersWithPaginationQueryHandler : GetWithPaginationQueryHandler<ApplicationUser, GetGroupUsersWithPaginationQuery, MemberDTO>
{
    public GetGroupUsersWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
    protected override Expression<Func<ApplicationUser, bool>> FilterPredicate(GetGroupUsersWithPaginationQuery query)
    {
        return x => x.GroupUsers.Any(x => x.GroupId == query.Id);
    }
    protected override Expression<Func<ApplicationUser, bool>> SearchStringPredicate(string searchString)
    {
        var toLowerSearchString = searchString.ToLower();
        return x => x.UserName.ToLower().Contains(toLowerSearchString) || x.Email.ToLower().Contains(toLowerSearchString);
    }
    protected override Expression<Func<ApplicationUser, object>> OrderByKeySelector()
    {
        return x => x.UserName;
    }
}
