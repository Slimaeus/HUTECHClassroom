using HUTECHClassroom.Application.Common.Validators;
using HUTECHClassroom.Application.Posts.DTOs;

namespace HUTECHClassroom.Application.Classrooms.Queries.GetClassroomPostsWithPagination;

public class GetClassroomPostsWithPaginationQueryValidator : GetWithPaginationQueryValidator<GetClassroomPostsWithPaginationQuery, PostDTO, ClassroomPaginationParams>
{
    public GetClassroomPostsWithPaginationQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotNull();
    }
}
