using HUTECHClassroom.Application.Common.Validators;
using HUTECHClassroom.Application.Posts.DTOs;

namespace HUTECHClassroom.Application.Posts.Queries.GetPostsWithPagination;

public sealed class GetPostsWithPaginationQueryValidator : GetWithPaginationQueryValidator<GetPostsWithPaginationQuery, PostDTO, PostPaginationParams> { }
