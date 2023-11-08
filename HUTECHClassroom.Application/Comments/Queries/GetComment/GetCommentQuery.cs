using HUTECHClassroom.Application.Comments.DTOs;
using HUTECHClassroom.Application.Common.Requests;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Comments.Queries.GetComment;

public record GetCommentQuery(Guid Id) : GetQuery<CommentDTO>;
public sealed class GetCommentQueryHandler : GetQueryHandler<Comment, GetCommentQuery, CommentDTO>
{
    public GetCommentQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }
    public override Expression<Func<Comment, bool>> FilterPredicate(GetCommentQuery query)
    {
        return x => x.Id == query.Id;
    }
    public override object GetNotFoundKey(GetCommentQuery query)
    {
        return query.Id;
    }
}
