using EntityFrameworkCore.QueryBuilder.Interfaces;
using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Posts.DTOs;
using HUTECHClassroom.Domain.Interfaces;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Users.Queries.GetUserPostsWithPagination;

public record GetUserPostsWithPaginationQuery(PaginationParams Params) : GetWithPaginationQuery<PostDTO, PaginationParams>(Params);
public class GetUserPostsWithPaginationQueryHandler : GetWithPaginationQueryHandler<Post, GetUserPostsWithPaginationQuery, PostDTO, PaginationParams>
{
    private readonly IUserAccessor _userAccessor;

    public GetUserPostsWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserAccessor userAccessor) : base(unitOfWork, mapper)
        => _userAccessor = userAccessor;
    protected override IQuery<Post> Order(IMultipleResultQuery<Post> query)
        => query.OrderByDescending(x => x.CreateDate);
    protected override Expression<Func<Post, bool>> FilterPredicate(GetUserPostsWithPaginationQuery query)
        => x => x.UserId == _userAccessor.Id;
}
