using EntityFrameworkCore.QueryBuilder.Interfaces;
using HUTECHClassroom.Application.Answers.DTOs;
using HUTECHClassroom.Application.Common.Extensions;
using HUTECHClassroom.Application.Common.Requests;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Answers.Queries.GetAnswersWithPagination;

public record GetAnswersWithPaginationQuery(AnswerPaginationParams Params) : GetWithPaginationQuery<AnswerDTO, AnswerPaginationParams>(Params);
public class GetAnswersWithPaginationQueryHandler : GetWithPaginationQueryHandler<Answer, GetAnswersWithPaginationQuery, AnswerDTO, AnswerPaginationParams>
{
    public GetAnswersWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }
    protected override Expression<Func<Answer, bool>> SearchStringPredicate(string searchString) =>
        x => x.Description.ToLower().Contains(searchString.ToLower())
        || x.Link.ToLower().Contains(searchString.ToLower());
    protected override IQuery<Answer> Order(IMultipleResultQuery<Answer> query) => query.OrderBy(x => x.CreateDate);
    protected override IMultipleResultQuery<Answer> SortingQuery(IMultipleResultQuery<Answer> query, GetAnswersWithPaginationQuery request)
        => query.SortEntityQuery(request.Params.DescriptionOrder, x => x.Description)
                .SortEntityQuery(request.Params.LinkOrder, x => x.Link)
                .SortEntityQuery(request.Params.ScoreOrder, x => x.Score);
}

