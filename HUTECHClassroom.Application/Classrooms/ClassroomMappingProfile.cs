using HUTECHClassroom.Application.Classrooms.Commands.AddClassroomUser;
using HUTECHClassroom.Application.Classrooms.Commands.CreateClassroom;
using HUTECHClassroom.Application.Classrooms.Commands.UpdateClassroom;
using HUTECHClassroom.Application.Classrooms.DTOs;
using HUTECHClassroom.Application.Common.DTOs;
using HUTECHClassroom.Application.Groups.DTOs;

namespace HUTECHClassroom.Application.Classrooms;

public sealed class ClassroomMappingProfile : Profile
{
    public ClassroomMappingProfile()
    {
        CreateMap<Classroom, ClassroomDTO>()
            .ForMember(x => x.Class, (config) => config.MapFrom(u => u.Class != null ? u.Class.Name : null));
        CreateMap<CreateClassroomCommand, Classroom>();
        CreateMap<UpdateClassroomCommand, Classroom>()
            .ForAllMembers(options => options.Condition((src, des, srcValue, desValue) => srcValue != null));

        CreateMap<ClassroomUser, MemberDTO>()
            .ConstructUsing(x => new MemberDTO(x.UserId, x.User != null ? x.User.UserName : null, x.User != null ? x.User.Email : null, x.User != null ? x.User.FirstName : null, x.User != null ? x.User.LastName : null, x.User != null && x.User.Class != null ? x.User.Class.Name : null, x.User != null && x.User.Avatar != null ? x.User.Avatar.Url : ""));

        CreateMap<ClassroomUser, ClassroomUserDTO>()
            .ConstructUsing((x) => new ClassroomUserDTO(x.UserId, x.User != null ? x.User.UserName : null, x.User != null ? x.User.Email : null, x.User != null ? x.User.FirstName : null, x.User != null ? x.User.LastName : null, x.User != null && x.User.Class != null ? x.User.Class.Name : null, new HashSet<GroupDTO>()))
            .ForMember(des => des.Groups, options => options.MapFrom(src => src.Classroom!.Groups.Where(g => g.GroupUsers.Any(gu => src.UserId == gu.UserId))));

        CreateMap<Exercise, ClassroomExerciseDTO>();
        CreateMap<Faculty, ClassroomFacultyDTO>();
        CreateMap<Group, ClassroomGroupDTO>();
        CreateMap<Post, ClassroomPostDTO>();
        CreateMap<Subject, ClassroomSubjectDTO>();
        CreateMap<Major, ClassroomSubjectMajorDTO>();

        CreateMap<AddClassroomUserCommand, ClassroomUser>();

        CreateMap<StudentResult, ClassroomScoreDTO>();
    }
}
