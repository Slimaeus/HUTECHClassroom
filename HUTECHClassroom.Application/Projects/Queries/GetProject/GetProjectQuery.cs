using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Projects.DTOs;
using HUTECHClassroom.Domain.Entities;

namespace HUTECHClassroom.Application.Projects.Queries.GetProject
{
    public record GetProjectQuery(Guid Id) : GetQuery<ProjectDTO>(Id);
    public class GetProjectQueryHandler : GetQueryHandler<Project, GetProjectQuery, ProjectDTO>
    {
        public GetProjectQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }
    }
}
