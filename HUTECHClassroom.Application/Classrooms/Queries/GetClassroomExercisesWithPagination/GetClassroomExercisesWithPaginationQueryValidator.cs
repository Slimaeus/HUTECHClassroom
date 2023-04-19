using FluentValidation;
using HUTECHClassroom.Application.Classrooms.DTOs;
using HUTECHClassroom.Application.Common.Validators;

namespace HUTECHClassroom.Application.Classrooms.Queries.GetClassroomExercisesWithPagination;

public class GetClassroomExercisesWithPaginationQueryValidator : GetWithPaginationQueryValidator<GetClassroomExercisesWithPaginationQuery, ClassroomExerciseDTO>
{
    public GetClassroomExercisesWithPaginationQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotNull();
    }
}
