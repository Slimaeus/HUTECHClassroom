﻿using EntityFrameworkCore.QueryBuilder.Interfaces;
using HUTECHClassroom.Application.Common.Extensions;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Posts.DTOs;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Posts.Queries.GetPostsWithPagination;

public sealed record GetPostsWithPaginationQuery(PostPaginationParams Params) : GetWithPaginationQuery<PostDTO, PostPaginationParams>(Params);
public sealed class GetPostsWithPaginationQueryHandler : GetWithPaginationQueryHandler<Post, GetPostsWithPaginationQuery, PostDTO, PostPaginationParams>
{
    public GetPostsWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }
    protected override Expression<Func<Post, bool>> FilterPredicate(GetPostsWithPaginationQuery query)
        => x => query.Params.UserId == null || query.Params.UserId == Guid.Empty || query.Params.UserId == x.UserId;
    protected override Expression<Func<Post, bool>> SearchStringPredicate(string searchString)
        => x => x.Content.ToLower().Contains(searchString.ToLower()) || (x.Link != null && x.Link.ToLower().Contains(searchString.ToLower()));
    protected override IQuery<Post> Order(IMultipleResultQuery<Post> query) => query.OrderByDescending(x => x.CreateDate);
    protected override IMultipleResultQuery<Post> SortingQuery(IMultipleResultQuery<Post> query, GetPostsWithPaginationQuery request)
        => query.SortEntityQuery(request.Params.ContentOrder, x => x.Content)
                .SortEntityQuery(request.Params.LinkOrder, x => x.Link);
}

