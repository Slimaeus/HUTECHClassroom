using EntityFrameworkCore.QueryBuilder.Interfaces;
using HUTECHClassroom.Application.Comments.DTOs;
using HUTECHClassroom.Application.Common.Extensions;
using HUTECHClassroom.Application.Common.Requests;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Comments.Queries.GetCommentsWithPagination;

public record GetCommentsWithPaginationQuery(CommentPaginationParams Params) : GetWithPaginationQuery<CommentDTO, CommentPaginationParams>(Params);
public class GetCommentsWithPaginationQueryHandler : GetWithPaginationQueryHandler<Comment, GetCommentsWithPaginationQuery, CommentDTO, CommentPaginationParams>
{
    public GetCommentsWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }
    protected override Expression<Func<Comment, bool>> SearchStringPredicate(string searchString)
        => x => x.Content.ToLower().Contains(searchString.ToLower());
    protected override Expression<Func<Comment, object>> OrderByKeySelector() => x => x.CreateDate;
    protected override IMultipleResultQuery<Comment> SortingQuery(IMultipleResultQuery<Comment> query, GetCommentsWithPaginationQuery request)
        => query.SortEntityQuery(request.Params.ContentOrder, x => x.Content);
}

