using HUTECHClassroom.Application.Classrooms.Commands.AddClassroomUser;
using HUTECHClassroom.Application.Classrooms.Commands.CreateClassroom;
using HUTECHClassroom.Application.Classrooms.Commands.UpdateClassroom;
using HUTECHClassroom.Application.Classrooms.DTOs;
using HUTECHClassroom.Application.Common.DTOs;
using HUTECHClassroom.Application.Groups.DTOs;

namespace HUTECHClassroom.Application.Classrooms;

public class ClassroomMappingProfile : Profile
{
    public ClassroomMappingProfile()
    {
        CreateMap<Classroom, ClassroomDTO>()
            .ForMember(x => x.Class, (config) => config.MapFrom(u => u.Class.Name));
        CreateMap<CreateClassroomCommand, Classroom>();
        CreateMap<UpdateClassroomCommand, Classroom>()
            .ForAllMembers(options => options.Condition((src, des, srcValue, desValue) => srcValue != null));

        CreateMap<ClassroomUser, MemberDTO>()
            .ConstructUsing(x => new MemberDTO(x.UserId, x.User.UserName, x.User.Email, x.User.FirstName, x.User.LastName, x.User.Avatar == null ? "" : x.User.Avatar.Url));

        CreateMap<ClassroomUser, ClassroomUserDTO>()
            .ConstructUsing((x) => new ClassroomUserDTO(x.UserId, x.User.UserName, x.User.Email, x.User.FirstName, x.User.LastName, new HashSet<GroupDTO>()))
            .ForMember(des => des.Groups, options => options.MapFrom(src => src.Classroom.Groups.Where(g => g.GroupUsers.Any(gu => src.UserId == gu.UserId))));

        CreateMap<Exercise, ClassroomExerciseDTO>();
        CreateMap<Faculty, ClassroomFacultyDTO>();
        CreateMap<Group, ClassroomGroupDTO>();
        CreateMap<Post, ClassroomPostDTO>();
        CreateMap<Subject, ClassroomSubjectDTO>();
        CreateMap<Major, ClassroomSubjectMajorDTO>();

        CreateMap<AddClassroomUserCommand, ClassroomUser>();
    }
}
