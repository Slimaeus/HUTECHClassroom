using AutoMapper;
using HUTECHClassroom.Application.Common.DTOs;
using HUTECHClassroom.Application.Groups.Commands.CreateGroup;
using HUTECHClassroom.Application.Groups.Commands.UpdateGroup;
using HUTECHClassroom.Application.Groups.DTOs;
using HUTECHClassroom.Application.Missions.Commands.CreateMission;
using HUTECHClassroom.Application.Missions.Commands.UpdateMission;
using HUTECHClassroom.Application.Missions.DTOs;
using HUTECHClassroom.Application.Projects.Commands.CreateProject;
using HUTECHClassroom.Application.Projects.Commands.UpdateProject;
using HUTECHClassroom.Application.Projects.DTOs;
using HUTECHClassroom.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace HUTECHClassroom.Application.Common.Mappings
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            #region User
            CreateMap<ApplicationUser, MemberDTO>();
            #endregion

            #region Missions
            CreateMap<Mission, MissionDTO>()
                .ForMember(x => x.Project, options => options.MapFrom(x => x.Project));
            CreateMap<CreateMissionCommand, Mission>();
            CreateMap<UpdateMissionCommand, Mission>()
                .ForAllMembers(options => options.Condition((src, des, srcValue, desValue) => srcValue != null));

            CreateMap<Project, MissionProjectDTO>();

            CreateMap<MissionUser, MemberDTO>()
                .ConstructUsing(x => new MemberDTO(x.User.UserName, x.User.Email));
            #endregion

            #region Projects
            CreateMap<Project, ProjectDTO>();
            CreateMap<CreateProjectCommand, Project>();
            CreateMap<UpdateProjectCommand, Project>()
                .ForAllMembers(options => options.Condition((src, des, srcValue, desValue) => srcValue != null));

            CreateMap<Mission, ProjectMissionDTO>();
            CreateMap<Group, ProjectGroupDTO>();
            #endregion

            #region Groups
            CreateMap<Group, GroupDTO>();
            CreateMap<CreateGroupCommand, Group>();
            CreateMap<UpdateGroupCommand, Group>()
                .ForAllMembers(options => options.Condition((src, des, srcValue, desValue) => srcValue != null));

            CreateMap<GroupUser, MemberDTO>()
                .ConstructUsing(x => new MemberDTO(x.User.UserName, x.User.Email));
            CreateMap<Project, GroupProjectDTO>();
            #endregion
        }
    }
}
