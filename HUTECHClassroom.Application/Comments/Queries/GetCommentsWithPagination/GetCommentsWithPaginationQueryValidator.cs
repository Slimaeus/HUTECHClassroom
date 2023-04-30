﻿using HUTECHClassroom.Application.Comments.DTOs;
using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Common.Validators;

namespace HUTECHClassroom.Application.Comments.Queries.GetCommentsWithPagination;

public class GetCommentsWithPaginationQueryValidator : GetWithPaginationQueryValidator<GetCommentsWithPaginationQuery, CommentDTO, PaginationParams>
{
    public GetCommentsWithPaginationQueryValidator()
    {
    }
}
