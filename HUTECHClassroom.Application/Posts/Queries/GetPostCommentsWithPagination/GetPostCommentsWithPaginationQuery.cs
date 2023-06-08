using EntityFrameworkCore.QueryBuilder.Interfaces;
using HUTECHClassroom.Application.Comments.DTOs;
using HUTECHClassroom.Application.Common.Extensions;
using HUTECHClassroom.Application.Common.Requests;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Posts.Queries.GetPostCommentsWithPagination;

public record GetPostCommentsWithPaginationQuery(Guid Id, PostPaginationParams Params) : GetWithPaginationQuery<CommentDTO, PostPaginationParams>(Params);
public class GetPostCommentsWithPaginationQueryHandler : GetWithPaginationQueryHandler<Comment, GetPostCommentsWithPaginationQuery, CommentDTO, PostPaginationParams>
{
    public GetPostCommentsWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }
    protected override Expression<Func<Comment, bool>> SearchStringPredicate(string searchString)
        => x => x.Content.ToLower().Contains(searchString.ToLower());
    protected override IQuery<Comment> Order(IMultipleResultQuery<Comment> query) => query.OrderByDescending(x => x.CreateDate);
    protected override Expression<Func<Comment, bool>> FilterPredicate(GetPostCommentsWithPaginationQuery query)
    {
        return x => x.PostId == query.Id && (query.Params.UserId == null || query.Params.UserId == Guid.Empty || query.Params.UserId == x.UserId);
    }

    protected override IMultipleResultQuery<Comment> SortingQuery(IMultipleResultQuery<Comment> query, GetPostCommentsWithPaginationQuery request)
        => query.SortEntityQuery(request.Params.ContentOrder, x => x.Content);
}

