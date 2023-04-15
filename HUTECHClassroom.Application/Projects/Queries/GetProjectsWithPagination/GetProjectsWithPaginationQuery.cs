using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Projects.DTOs;
using HUTECHClassroom.Domain.Entities;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Projects.Queries.GetProjectsWithPagination
{
    public record GetProjectsWithPaginationQuery(PaginationParams Params) : GetWithPaginationQuery<ProjectDTO>(Params);
    public class GetProjectsWithPaginationQueryHandler : GetWithPaginationQueryHandler<Project, GetProjectsWithPaginationQuery, ProjectDTO>
    {
        public GetProjectsWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        public override Expression<Func<Project, bool>> SearchStringPredicate(string searchString) =>
            x => x.Name.ToLower().Contains(searchString.ToLower()) || x.Description.ToLower().Contains(searchString.ToLower());
    }
}

