using HUTECHClassroom.Application.Common.Validators;
using HUTECHClassroom.Application.Exercises.DTOs;

namespace HUTECHClassroom.Application.Classrooms.Queries.GetClassroomExercisesWithPagination;

public class GetClassroomExercisesWithPaginationQueryValidator : GetWithPaginationQueryValidator<GetClassroomExercisesWithPaginationQuery, ExerciseDTO, ClassroomPaginationParams>
{
    public GetClassroomExercisesWithPaginationQueryValidator() : base()
    {
        RuleFor(x => x.Id).NotEmpty().NotNull();
    }
}
