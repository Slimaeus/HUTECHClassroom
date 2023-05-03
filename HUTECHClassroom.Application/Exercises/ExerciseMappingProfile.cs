using HUTECHClassroom.Application.Common.DTOs;
using HUTECHClassroom.Application.Exercises.Commands.CreateExercise;
using HUTECHClassroom.Application.Exercises.Commands.UpdateGroup;
using HUTECHClassroom.Application.Exercises.DTOs;

namespace HUTECHClassroom.Application.Exercises;

public class ExerciseMappingProfile : Profile
{
    public ExerciseMappingProfile()
    {
        CreateMap<Exercise, ExerciseDTO>();
        CreateMap<CreateExerciseCommand, Exercise>();
        CreateMap<UpdateExerciseCommand, Exercise>()
            .ForAllMembers(options => options.Condition((src, des, srcValue, desValue) => srcValue != null));

        CreateMap<ExerciseUser, MemberDTO>()
            .ConstructUsing(x => new MemberDTO(x.User.UserName, x.User.Email, x.User.FirstName, x.User.LastName));
        CreateMap<Classroom, ExerciseClassroomDTO>();
    }
}
