using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using HUTECHClassroom.Application.Common.DTOs;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Domain.Entities;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Groups.Queries.GetGroupUser;

public record GetGroupUserQuery(Guid Id, string UserName) : GetQuery<MemberDTO>;
public class GetGroupUserQueryHandler : GetQueryHandler<ApplicationUser, GetGroupUserQuery, MemberDTO>
{
    public GetGroupUserQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

    public override Expression<Func<ApplicationUser, bool>> FilterPredicate(GetGroupUserQuery query)
    {
        return x => x.UserName == query.UserName && x.GroupUsers.Any(x => x.GroupId == query.Id);
    }
    public override object GetNotFoundKey(GetGroupUserQuery query)
    {
        return query.UserName;
    }
}