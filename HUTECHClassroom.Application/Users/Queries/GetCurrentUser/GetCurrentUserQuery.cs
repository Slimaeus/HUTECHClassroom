using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Users.DTOs;
using HUTECHClassroom.Domain.Interfaces;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Users.Queries.GetCurrentUser;

public record GetCurrentUserQuery() : GetQuery<UserDTO>;
public class GetCurrentUserQueryHandler : GetQueryHandler<ApplicationUser, GetCurrentUserQuery, UserDTO>
{
    private readonly IUserAccessor _userAccessor;

    public GetCurrentUserQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserAccessor userAccessor) : base(unitOfWork, mapper)
    {
        _userAccessor = userAccessor;
    }
    public override Expression<Func<ApplicationUser, bool>> FilterPredicate(GetCurrentUserQuery query)
    {
        return x => x.Id == _userAccessor.Id;
    }
    public override object GetNotFoundKey(GetCurrentUserQuery query)
    {
        return _userAccessor.UserName;
    }
}