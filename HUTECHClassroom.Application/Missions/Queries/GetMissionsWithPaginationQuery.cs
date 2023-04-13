using AutoMapper;
using AutoMapper.QueryableExtensions;
using EntityFrameworkCore.QueryBuilder.Interfaces;
using EntityFrameworkCore.Repository.Collections;
using EntityFrameworkCore.Repository.Extensions;
using EntityFrameworkCore.Repository.Interfaces;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using HUTECHClassroom.Application.Common.Exceptions;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Missions.DTOs;
using HUTECHClassroom.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Missions.Queries
{
    public record GetMissionsWithPaginationQuery : GetWithPaginationQuery<Mission, MissionDTO>;
    public class GetMissionsWithPaginationQueryHandler : GetWithPaginationQueryHandler<Mission, GetMissionsWithPaginationQuery, MissionDTO>
    {
        public GetMissionsWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public override Expression<Func<Mission, bool>> SearchStringPredicate(string searchString)
        {
            return x =>
                x.Title.ToLower().Contains(searchString.ToLower())
                || x.Description.ToLower().Contains(searchString.ToLower());
        }
    }
}

