using HUTECHClassroom.Application.Comments;
using HUTECHClassroom.Application.Common.Validators;
using HUTECHClassroom.Application.Posts.DTOs;

namespace HUTECHClassroom.Application.Posts.Queries.GetPostCommentsWithPagination;

public class GetPostCommentsWithPaginationQueryValidator : GetWithPaginationQueryValidator<GetPostCommentsWithPaginationQuery, PostCommentDTO, CommentPaginationParams>
{
    public GetPostCommentsWithPaginationQueryValidator()
    {
    }
}
