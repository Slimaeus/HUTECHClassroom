using HUTECHClassroom.Application.Comments.DTOs;
using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Common.Validators;

namespace HUTECHClassroom.Application.Users.Queries.GetUserCommentsWithPagination;

public class GetUserComemnsWithPaginationQueryValidator : GetWithPaginationQueryValidator<GetUserCommentsWithPaginationQuery, CommentDTO, PaginationParams>
{
}
