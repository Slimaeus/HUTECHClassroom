using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Groups.DTOs;
using HUTECHClassroom.Domain.Interfaces;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Groups.Queries.GetGroup;

public record GetGroupQuery(Guid Id) : GetQuery<GroupDTO>;
public sealed class GetGroupQueryHandler : GetQueryHandler<Group, GetGroupQuery, GroupDTO>
{
    private readonly IUserAccessor _userAccessor;

    public GetGroupQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserAccessor userAccessor) : base(unitOfWork, mapper)
    {
        _userAccessor = userAccessor;
    }
    protected override object GetMappingParameters()
    {
        return new { currentUserId = _userAccessor.Id };
    }
    public override Expression<Func<Group, bool>> FilterPredicate(GetGroupQuery query)
    {
        return x => x.Id == query.Id;
    }
    public override object GetNotFoundKey(GetGroupQuery query)
    {
        return query.Id;
    }
}
