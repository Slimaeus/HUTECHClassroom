using HUTECHClassroom.Application.Classrooms.DTOs;
using HUTECHClassroom.Application.Common.Validators;

namespace HUTECHClassroom.Application.Classrooms.Queries.GetClassroomPostsWithPagination;

public class GetClassroomPostsWithPaginationQueryValidator : GetWithPaginationQueryValidator<GetClassroomPostsWithPaginationQuery, ClassroomPostDTO>
{
    public GetClassroomPostsWithPaginationQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotNull();
    }
}
