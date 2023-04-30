using HUTECHClassroom.Application.Classrooms.DTOs;
using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Common.Validators;

namespace HUTECHClassroom.Application.Classrooms.Queries.GetClassroomExercisesWithPagination;

public class GetClassroomExercisesWithPaginationQueryValidator : GetWithPaginationQueryValidator<GetClassroomExercisesWithPaginationQuery, ClassroomExerciseDTO, PaginationParams>
{
    public GetClassroomExercisesWithPaginationQueryValidator() : base()
    {
        RuleFor(x => x.Id).NotEmpty().NotNull();
    }
}
