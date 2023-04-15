using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Projects.DTOs;
using HUTECHClassroom.Domain.Entities;

namespace HUTECHClassroom.Application.Projects.Commands.CreateProject
{
    public record CreateProjectCommand : CreateCommand<ProjectDTO>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class CreateProjectCommandHandler : CreateCommandHandler<Project, CreateProjectCommand, ProjectDTO>
    {
        public CreateProjectCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
    }
}
