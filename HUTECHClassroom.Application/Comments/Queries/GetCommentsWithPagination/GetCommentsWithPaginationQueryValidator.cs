using HUTECHClassroom.Application.Comments.DTOs;
using HUTECHClassroom.Application.Common.Validators;

namespace HUTECHClassroom.Application.Comments.Queries.GetCommentsWithPagination;

public sealed class GetCommentsWithPaginationQueryValidator : GetWithPaginationQueryValidator<GetCommentsWithPaginationQuery, CommentDTO, CommentPaginationParams>
{
    public GetCommentsWithPaginationQueryValidator()
    {
    }
}
