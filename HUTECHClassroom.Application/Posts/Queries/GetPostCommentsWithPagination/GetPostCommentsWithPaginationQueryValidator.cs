using HUTECHClassroom.Application.Comments.DTOs;
using HUTECHClassroom.Application.Common.Validators;

namespace HUTECHClassroom.Application.Posts.Queries.GetPostCommentsWithPagination;

public class GetPostCommentsWithPaginationQueryValidator : GetWithPaginationQueryValidator<GetPostCommentsWithPaginationQuery, CommentDTO, PostPaginationParams>
{
    public GetPostCommentsWithPaginationQueryValidator()
    {
    }
}
