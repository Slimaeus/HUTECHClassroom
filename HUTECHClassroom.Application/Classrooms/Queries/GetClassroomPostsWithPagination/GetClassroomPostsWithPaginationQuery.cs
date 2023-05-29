using EntityFrameworkCore.QueryBuilder.Interfaces;
using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Posts.DTOs;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Classrooms.Queries.GetClassroomPostsWithPagination;

public record GetClassroomPostsWithPaginationQuery(Guid Id, PaginationParams Params) : GetWithPaginationQuery<PostDTO, PaginationParams>(Params);
public class GetClassroomPostsWithPaginationQueryHandler : GetWithPaginationQueryHandler<Post, GetClassroomPostsWithPaginationQuery, PostDTO, PaginationParams>
{
    public GetClassroomPostsWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
    protected override Expression<Func<Post, bool>> FilterPredicate(GetClassroomPostsWithPaginationQuery query)
    {
        return x => x.ClassroomId == query.Id;
    }
    protected override Expression<Func<Post, bool>> SearchStringPredicate(string searchString)
    {
        var toLowerSearchString = searchString.ToLower();
        return x => x.Content.ToLower().Contains(toLowerSearchString) || x.Link.ToLower().Contains(toLowerSearchString);
    }
    protected override IQuery<Post> Order(IMultipleResultQuery<Post> query) => query.OrderByDescending(x => x.CreateDate);

}
