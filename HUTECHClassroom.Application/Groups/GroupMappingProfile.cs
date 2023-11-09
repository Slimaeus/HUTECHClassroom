using HUTECHClassroom.Application.Common.DTOs;
using HUTECHClassroom.Application.Groups.Commands.AddGroupLeader;
using HUTECHClassroom.Application.Groups.Commands.AddGroupUser;
using HUTECHClassroom.Application.Groups.Commands.CreateGroup;
using HUTECHClassroom.Application.Groups.Commands.UpdateGroup;
using HUTECHClassroom.Application.Groups.DTOs;
using HUTECHClassroom.Domain.Constants;

namespace HUTECHClassroom.Application.Groups;

public sealed class GroupMappingProfile : Profile
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
            .ConstructUsing(x => new MemberDTO(x.UserId, x.User != null ? x.User.UserName : null, x.User != null ? x.User.Email : null, x.User != null ? x.User.FirstName : null, x.User != null ? x.User.LastName : null, x.User != null && x.User.Class != null ? x.User.Class.Name : null, x.User != null && x.User.Avatar != null ? x.User.Avatar.Url : string.Empty));

        CreateMap<Project, GroupProjectDTO>();
        CreateMap<Classroom, GroupClassroomDTO>()
            .ForMember(x => x.Class, (config) => config.MapFrom(u => u.Class != null ? u.Class.Name : null));

        CreateMap<GroupUser, GroupUserDTO>()
            .ConstructUsing(x => new GroupUserDTO(x.UserId, x.User != null ? x.User.UserName : null, x.User != null ? x.User.Email : null, x.User != null ? x.User.FirstName : null, x.User != null ? x.User.LastName : null, x.GroupRole != null ? x.GroupRole.Name : null));

        CreateMap<AddGroupUserCommand, GroupUser>();
        CreateMap<AddGroupLeaderCommand, GroupUser>();
    }
}
