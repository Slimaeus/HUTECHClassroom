using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using HUTECHClassroom.Application.Common.DTOs;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Domain.Entities;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Missions.Queries.GetMissionUser;

public record GetMissionUserQuery(Guid Id, string UserName) : GetQuery<MemberDTO>;
public class GetMissionUserQueryHandler : GetQueryHandler<ApplicationUser, GetMissionUserQuery, MemberDTO>
{
    public GetMissionUserQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

    public override Expression<Func<ApplicationUser, bool>> FilterPredicate(GetMissionUserQuery query)
    {
        return x => x.UserName == query.UserName && x.MissionUsers.Any(x => x.MissionId == query.Id);
    }
    public override object GetNotFoundKey(GetMissionUserQuery query)
    {
        return query.UserName;
    }
}