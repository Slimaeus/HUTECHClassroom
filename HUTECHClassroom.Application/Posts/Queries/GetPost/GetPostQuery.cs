using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Posts.DTOs;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Posts.Queries.GetPost;

public record GetPostQuery(Guid Id) : GetQuery<PostDTO>;
public sealed class GetPostQueryHandler : GetQueryHandler<Post, GetPostQuery, PostDTO>
{
    public GetPostQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }
    public override Expression<Func<Post, bool>> FilterPredicate(GetPostQuery query)
    {
        return x => x.Id == query.Id;
    }
    public override object GetNotFoundKey(GetPostQuery query)
    {
        return query.Id;
    }
}
