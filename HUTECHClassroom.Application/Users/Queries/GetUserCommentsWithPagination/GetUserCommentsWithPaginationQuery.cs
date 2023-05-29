using EntityFrameworkCore.QueryBuilder.Interfaces;
using HUTECHClassroom.Application.Comments.DTOs;
using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Domain.Interfaces;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Users.Queries.GetUserCommentsWithPagination;

public record GetUserCommentsWithPaginationQuery(PaginationParams Params) : GetWithPaginationQuery<CommentDTO, PaginationParams>(Params);
public class GetUserCommentsWithPaginationQueryHandler : GetWithPaginationQueryHandler<Comment, GetUserCommentsWithPaginationQuery, CommentDTO, PaginationParams>
{
    private readonly IUserAccessor _userAccessor;

    public GetUserCommentsWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserAccessor userAccessor) : base(unitOfWork, mapper)
    {
        _userAccessor = userAccessor;
    }
    protected override IQuery<Comment> Order(IMultipleResultQuery<Comment> query) => query.OrderByDescending(x => x.CreateDate);
    protected override Expression<Func<Comment, bool>> FilterPredicate(GetUserCommentsWithPaginationQuery query)
        => x => x.UserId == _userAccessor.Id;
}
