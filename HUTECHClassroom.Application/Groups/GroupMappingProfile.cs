using HUTECHClassroom.Application.Common.DTOs;
using HUTECHClassroom.Application.Groups.Commands.AddGroupLeader;
using HUTECHClassroom.Application.Groups.Commands.AddGroupUser;
using HUTECHClassroom.Application.Groups.Commands.CreateGroup;
using HUTECHClassroom.Application.Groups.Commands.UpdateGroup;
using HUTECHClassroom.Application.Groups.DTOs;
using HUTECHClassroom.Domain.Constants;

namespace HUTECHClassroom.Application.Groups;

public class GroupMappingProfile : Profile
{
    public GroupMappingProfile()
    {
        Guid? UserId = null;
        CreateMap<Group, GroupDTO>()
            .ForMember(x => x.Roles, config => config.MapFrom(g =>
            (g.LeaderId == UserId)
            ? new List<string> { GroupRoleConstants.LEADER }
            : g.GroupUsers.Any(gu => gu.UserId == UserId)
                ? new List<string> { GroupRoleConstants.MEMBER }
                : new List<string>()));
        CreateMap<CreateGroupCommand, Group>();
        CreateMap<UpdateGroupCommand, Group>()
            .ForAllMembers(options => options.Condition((src, des, srcValue, desValue) => srcValue != null));

        CreateMap<GroupUser, MemberDTO>()
            .ConstructUsing(x => new MemberDTO(x.UserId, x.User.UserName, x.User.Email, x.User.FirstName, x.User.LastName, x.User.Avatar == null ? "" : x.User.Avatar.Url));
        CreateMap<Project, GroupProjectDTO>();
        CreateMap<Classroom, GroupClassroomDTO>()
            .ForMember(x => x.Class, (config) => config.MapFrom(u => u.Class.Name));

        CreateMap<GroupUser, GroupUserDTO>()
            .ConstructUsing(x => new GroupUserDTO(x.UserId, x.User.UserName, x.User.Email, x.User.FirstName, x.User.LastName, x.GroupRole.Name));

        CreateMap<AddGroupUserCommand, GroupUser>();
        CreateMap<AddGroupLeaderCommand, GroupUser>();
    }
}
