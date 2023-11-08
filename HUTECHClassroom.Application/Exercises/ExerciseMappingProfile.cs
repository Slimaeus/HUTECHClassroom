using HUTECHClassroom.Application.Common.DTOs;
using HUTECHClassroom.Application.Exercises.Commands.AddExerciseUser;
using HUTECHClassroom.Application.Exercises.Commands.CreateExercise;
using HUTECHClassroom.Application.Exercises.Commands.UpdateGroup;
using HUTECHClassroom.Application.Exercises.DTOs;

namespace HUTECHClassroom.Application.Exercises;

public sealed class ExerciseMappingProfile : Profile
{
    public ExerciseMappingProfile()
    {
        CreateMap<Exercise, ExerciseDTO>();
        CreateMap<CreateExerciseCommand, Exercise>();
        CreateMap<UpdateExerciseCommand, Exercise>()
            .ForAllMembers(options => options.Condition((src, des, srcValue, desValue) => srcValue != null));

        CreateMap<ExerciseUser, MemberDTO>()
            .ConstructUsing(x => new MemberDTO(x.UserId, x.User != null ? x.User.UserName : null, x.User != null ? x.User.Email : null, x.User != null ? x.User.FirstName : null, x.User != null ? x.User.LastName : null, x.User != null && x.User.Class != null ? x.User.Class.Name : null, x.User != null && x.User.Avatar != null && x.User.Avatar != null ? x.User.Avatar.Url : string.Empty));
        CreateMap<ExerciseUser, ExerciseUserDTO>()
            .ConstructUsing(x => new ExerciseUserDTO(x.UserId, x.User != null ? x.User.UserName : null, x.User != null ? x.User.Email : null, x.User != null ? x.User.FirstName : null, x.User != null ? x.User.LastName : null, x.Exercise != null && x.Exercise.Answers.Any(a => a.UserId == x.UserId)));
        CreateMap<Classroom, ExerciseClassroomDTO>()
            .ForMember(x => x.Class, (config) => config.MapFrom(u => u.Class != null ? u.Class.Name : null));

        CreateMap<AddExerciseUserCommand, ExerciseUser>();
    }
}
