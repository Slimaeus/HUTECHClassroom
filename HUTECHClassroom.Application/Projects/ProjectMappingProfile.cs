﻿using HUTECHClassroom.Application.Projects.Commands.CreateProject;
using HUTECHClassroom.Application.Projects.Commands.UpdateProject;
using HUTECHClassroom.Application.Projects.DTOs;

namespace HUTECHClassroom.Application.Projects;

public sealed class ProjectMappingProfile : Profile
{
    public ProjectMappingProfile()
    {
        CreateMap<Project, ProjectDTO>();
        CreateMap<CreateProjectCommand, Project>();
        CreateMap<UpdateProjectCommand, Project>()
            .ForAllMembers(options => options.Condition((src, des, srcValue, desValue) => srcValue != null));

        CreateMap<Mission, ProjectMissionDTO>();
        CreateMap<Group, ProjectGroupDTO>();
    }
}
