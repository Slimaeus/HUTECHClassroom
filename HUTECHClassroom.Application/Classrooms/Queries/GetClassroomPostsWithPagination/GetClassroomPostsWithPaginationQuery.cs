using EntityFrameworkCore.QueryBuilder.Interfaces;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Posts.DTOs;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Classrooms.Queries.GetClassroomPostsWithPagination;

public sealed record GetClassroomPostsWithPaginationQuery(Guid Id, ClassroomPaginationParams Params) : GetWithPaginationQuery<PostDTO, ClassroomPaginationParams>(Params);
public sealed class GetClassroomPostsWithPaginationQueryHandler : GetWithPaginationQueryHandler<Post, GetClassroomPostsWithPaginationQuery, PostDTO, ClassroomPaginationParams>
{
    public GetClassroomPostsWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
    protected override Expression<Func<Post, bool>> FilterPredicate(GetClassroomPostsWithPaginationQuery query)
        => x => x.ClassroomId == query.Id && (query.Params.UserId == null || query.Params.UserId == Guid.Empty || query.Params.UserId == x.UserId);
    protected override Expression<Func<Post, bool>> SearchStringPredicate(string searchString)
    {
        var toLowerSearchString = searchString.ToLower();
        return x => x.Content.ToLower().Contains(toLowerSearchString) || (x.Link != null && x.Link.ToLower().Contains(toLowerSearchString));
    }
    protected override IQuery<Post> Order(IMultipleResultQuery<Post> query)
        => query.OrderByDescending(x => x.CreateDate);

}
