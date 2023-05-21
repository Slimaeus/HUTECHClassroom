using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Groups.DTOs;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Groups.Queries.GetGroupUser;

public record GetGroupUserQuery(Guid Id, string UserName) : GetQuery<GroupUserDTO>;
public class GetGroupUserQueryHandler : GetQueryHandler<GroupUser, GetGroupUserQuery, GroupUserDTO>
{
    public GetGroupUserQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

    public override Expression<Func<GroupUser, bool>> FilterPredicate(GetGroupUserQuery query)
    {
        return x => x.User.UserName == query.UserName && x.GroupId == query.Id;
    }
    public override object GetNotFoundKey(GetGroupUserQuery query)
    {
        return query.UserName;
    }
}