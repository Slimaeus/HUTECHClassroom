using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Posts.DTOs;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Posts.Queries.GetPostCommentsWithPagination;

public record GetPostCommentsWithPaginationQuery(Guid Id, PaginationParams Params) : GetWithPaginationQuery<PostCommentDTO>(Params);
public class GetPostCommentsWithPaginationQueryHandler : GetWithPaginationQueryHandler<Comment, GetPostCommentsWithPaginationQuery, PostCommentDTO>
{
    public GetPostCommentsWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }
    protected override Expression<Func<Comment, bool>> SearchStringPredicate(string searchString) =>
        x => x.Content.ToLower().Contains(searchString.ToLower());
    protected override Expression<Func<Comment, object>> OrderByKeySelector()
    {
        return x => x.CreateDate;
    }
    protected override Expression<Func<Comment, bool>> FilterPredicate(GetPostCommentsWithPaginationQuery query)
    {
        return x => x.PostId == query.Id;
    }
}

