using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Projects.DTOs;
using HUTECHClassroom.Domain.Entities;

namespace HUTECHClassroom.Application.Projects.Commands.DeleteProject;

public record DeleteProjectCommand(Guid Id) : DeleteCommand<ProjectDTO>(Id);
public class DeleteProjectCommandHandler : DeleteCommandHandler<Project, DeleteProjectCommand, ProjectDTO>
{
    public DeleteProjectCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}
