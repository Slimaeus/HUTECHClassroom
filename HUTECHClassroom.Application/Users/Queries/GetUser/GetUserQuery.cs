using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Users.DTOs;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Users.Queries.GetUser;

public record GetUserQuery(Guid Id) : GetQuery<UserDTO>;
public class GetUserQueryHandler : GetQueryHandler<ApplicationUser, GetUserQuery, UserDTO>
{
    public GetUserQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
    public override Expression<Func<ApplicationUser, bool>> FilterPredicate(GetUserQuery query)
    {
        return x => x.Id == query.Id;
    }
    public override object GetNotFoundKey(GetUserQuery query)
    {
        return query.Id;
    }
}
