using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Posts.DTOs;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Posts.Queries.GetPostsWithPagination;

public record GetPostsWithPaginationQuery(PaginationParams Params) : GetWithPaginationQuery<PostDTO>(Params);
public class GetPostsWithPaginationQueryHandler : GetWithPaginationQueryHandler<Post, GetPostsWithPaginationQuery, PostDTO>
{
    public GetPostsWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }
    protected override Expression<Func<Post, bool>> SearchStringPredicate(string searchString) =>
        x => x.Content.ToLower().Contains(searchString.ToLower()) || x.Link.ToLower().Contains(searchString.ToLower());
    protected override Expression<Func<Post, object>> OrderByKeySelector()
    {
        return x => x.CreateDate;
    }
}

