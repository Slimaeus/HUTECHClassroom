using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Common.Validators;
using HUTECHClassroom.Application.Posts.DTOs;

namespace HUTECHClassroom.Application.Classrooms.Queries.GetClassroomPostsWithPagination;

public class GetClassroomPostsWithPaginationQueryValidator : GetWithPaginationQueryValidator<GetClassroomPostsWithPaginationQuery, PostDTO, PaginationParams>
{
    public GetClassroomPostsWithPaginationQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotNull();
    }
}
