using AutoMapper;
using AutoMapper.QueryableExtensions;
using EntityFrameworkCore.QueryBuilder.Interfaces;
using EntityFrameworkCore.Repository.Collections;
using EntityFrameworkCore.Repository.Extensions;
using EntityFrameworkCore.Repository.Interfaces;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using HUTECHClassroom.Application.Common.Exceptions;
using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Projects.DTOs;
using HUTECHClassroom.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Projects.Queries.GetProjectMissionsWithPagination
{
    public record GetProjectMissionsWithPaginationQuery(Guid Id, PaginationParams Params) : GetWithPaginationQuery<ProjectMissionDTO>(Params);
    public class GetProjectMissionsWithPaginationQueryHandler : GetWithPaginationQueryHandler<Mission, GetProjectMissionsWithPaginationQuery, ProjectMissionDTO>
    {
        public GetProjectMissionsWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
        protected override Expression<Func<Mission, bool>> FilterPredicate(GetProjectMissionsWithPaginationQuery query)
        {
            return x => x.ProjectId == query.Id;
        }
        protected override Expression<Func<Mission, object>> OrderByKeySelector()
        {
            return x => x.CreateDate;
        }
        protected override Expression<Func<Mission, bool>> SearchStringPredicate(string searchString)
        {
            var toLowerSearchString = searchString.ToLower();
            return x => x.Title.ToLower().Contains(toLowerSearchString) || x.Description.ToLower().Contains(toLowerSearchString);
        }
    }
}
