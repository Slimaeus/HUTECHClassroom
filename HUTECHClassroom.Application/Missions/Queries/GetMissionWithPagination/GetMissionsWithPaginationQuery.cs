using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Missions.DTOs;
using HUTECHClassroom.Domain.Entities;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Missions.Queries.GetMissionWithPagination
{
    public record GetMissionsWithPaginationQuery : GetWithPaginationQuery<MissionDTO>;
    public class GetMissionsWithPaginationQueryHandler : GetWithPaginationQueryHandler<Mission, GetMissionsWithPaginationQuery, MissionDTO>
    {
        public GetMissionsWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        public override Expression<Func<Mission, bool>> SearchStringPredicate(string searchString) =>
            x => x.Title.ToLower().Contains(searchString.ToLower()) || x.Description.ToLower().Contains(searchString.ToLower());
    }
}

