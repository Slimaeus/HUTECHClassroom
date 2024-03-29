﻿using EntityFrameworkCore.QueryBuilder.Interfaces;
using HUTECHClassroom.Application.Common.Extensions;
using HUTECHClassroom.Application.Common.Interfaces;
using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Missions.DTOs;
using HUTECHClassroom.Domain.Interfaces;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Missions.Queries.GetMissionsWithPagination;

public sealed record GetMissionsWithPaginationQuery(MissionPaginationParams Params) : GetWithPaginationQuery<MissionDTO, MissionPaginationParams>(Params);
public sealed class GetMissionsWithPaginationQueryHandler : GetWithPaginationQueryHandler<Mission, GetMissionsWithPaginationQuery, MissionDTO, MissionPaginationParams>
{
    private readonly IUserAccessor _userAccessor;

    public GetMissionsWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserAccessor userAccessor) : base(unitOfWork, mapper)
        => _userAccessor = userAccessor;
    protected override Expression<Func<Mission, bool>> FilterPredicate(GetMissionsWithPaginationQuery query)
        => x => query.Params.UserId == null || query.Params.UserId == Guid.Empty || x.MissionUsers.Any(mu => query.Params.UserId == mu.UserId);
    protected override IMappingParams GetMappingParameters()
        => new UserMappingParams { UserId = _userAccessor.Id };
    protected override Expression<Func<Mission, bool>> SearchStringPredicate(string searchString)
        => x => x.Title.ToLower().Contains(searchString.ToLower()) || (x.Description != null && x.Description.ToLower().Contains(searchString.ToLower()));
    protected override IQuery<Mission> Order(IMultipleResultQuery<Mission> query)
        => query.OrderByDescending(x => x.CreateDate);

    protected override IMultipleResultQuery<Mission> SortingQuery(IMultipleResultQuery<Mission> query, GetMissionsWithPaginationQuery request)
        => query.SortEntityQuery(request.Params.TitleOrder, x => x.Title)
                .SortEntityQuery(request.Params.DescriptionOrder, x => x.Description)
                .SortEntityQuery(request.Params.IsDoneOrder, x => x.IsDone);
}

