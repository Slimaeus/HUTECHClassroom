using EntityFrameworkCore.QueryBuilder.Interfaces;
using HUTECHClassroom.Application.Answers.DTOs;
using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Domain.Interfaces;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Users.Queries.GetUserAnswersWithPagination;

public record GetUserAnswersWithPaginationQuery(PaginationParams Params) : GetWithPaginationQuery<AnswerDTO, PaginationParams>(Params);
public class GetUserAnswersWithPaginationQueryHandler : GetWithPaginationQueryHandler<Answer, GetUserAnswersWithPaginationQuery, AnswerDTO, PaginationParams>
{
    private readonly IUserAccessor _userAccessor;

    public GetUserAnswersWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserAccessor userAccessor) : base(unitOfWork, mapper)
    {
        _userAccessor = userAccessor;
    }
    protected override IQuery<Answer> Order(IMultipleResultQuery<Answer> query) => query.OrderBy(x => x.CreateDate);
    protected override Expression<Func<Answer, bool>> FilterPredicate(GetUserAnswersWithPaginationQuery query)
        => x => x.UserId == _userAccessor.Id;
}
