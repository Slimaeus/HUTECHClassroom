using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Common.Validators;
using HUTECHClassroom.Application.Posts.DTOs;

namespace HUTECHClassroom.Application.Users.Queries.GetUserPostsWithPagination;

public class GetUserPostsWithPaginationQueryValidator : GetWithPaginationQueryValidator<GetUserPostsWithPaginationQuery, PostDTO, PaginationParams>
{
}
