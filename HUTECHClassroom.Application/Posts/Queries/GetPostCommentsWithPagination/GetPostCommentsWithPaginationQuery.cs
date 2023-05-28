using EntityFrameworkCore.QueryBuilder.Interfaces;
using HUTECHClassroom.Application.Comments;
using HUTECHClassroom.Application.Comments.DTOs;
using HUTECHClassroom.Application.Common.Extensions;
using HUTECHClassroom.Application.Common.Requests;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Posts.Queries.GetPostCommentsWithPagination;

public record GetPostCommentsWithPaginationQuery(Guid Id, CommentPaginationParams Params) : GetWithPaginationQuery<CommentDTO, CommentPaginationParams>(Params);
public class GetPostCommentsWithPaginationQueryHandler : GetWithPaginationQueryHandler<Comment, GetPostCommentsWithPaginationQuery, CommentDTO, CommentPaginationParams>
{
    public GetPostCommentsWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }
    protected override Expression<Func<Comment, bool>> SearchStringPredicate(string searchString)
        => x => x.Content.ToLower().Contains(searchString.ToLower());
    protected override IQuery<Comment> Order(IMultipleResultQuery<Comment> query) => query.OrderByDescending(x => x.CreateDate);
    protected override Expression<Func<Comment, bool>> FilterPredicate(GetPostCommentsWithPaginationQuery query) => x => x.PostId == query.Id;
    protected override IMultipleResultQuery<Comment> SortingQuery(IMultipleResultQuery<Comment> query, GetPostCommentsWithPaginationQuery request)
        => query.SortEntityQuery(request.Params.ContentOrder, x => x.Content);
}

