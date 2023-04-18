using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Groups.DTOs;
using HUTECHClassroom.Domain.Entities;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Groups.Queries.GetGroup;

public record GetGroupQuery(Guid Id) : GetQuery<GroupDTO>;
public class GetGroupQueryHandler : GetQueryHandler<Group, GetGroupQuery, GroupDTO>
{
    public GetGroupQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
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
