using HUTECHClassroom.Application.Common.DTOs;
using HUTECHClassroom.Application.Groups.Commands.CreateGroup;
using HUTECHClassroom.Application.Groups.Commands.UpdateGroup;
using HUTECHClassroom.Application.Groups.DTOs;

namespace HUTECHClassroom.Application.Groups;

public class GroupMappingProfile : Profile
{
    public GroupMappingProfile()
    {
        CreateMap<Group, GroupDTO>();
        CreateMap<CreateGroupCommand, Group>();
        CreateMap<UpdateGroupCommand, Group>()
            .ForAllMembers(options => options.Condition((src, des, srcValue, desValue) => srcValue != null));

        CreateMap<GroupUser, MemberDTO>()
            .ConstructUsing(x => new MemberDTO(x.User.UserName, x.User.Email));
        CreateMap<Project, GroupProjectDTO>();
        CreateMap<Classroom, GroupClassroomDTO>();
    }
}
