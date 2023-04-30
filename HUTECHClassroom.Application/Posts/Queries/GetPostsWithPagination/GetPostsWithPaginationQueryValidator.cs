using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Common.Validators;
using HUTECHClassroom.Application.Posts.DTOs;

namespace HUTECHClassroom.Application.Posts.Queries.GetPostsWithPagination;

public class GetPostsWithPaginationQueryValidator : GetWithPaginationQueryValidator<GetPostsWithPaginationQuery, PostDTO, PaginationParams>
{
    public GetPostsWithPaginationQueryValidator()
    {
    }
}
