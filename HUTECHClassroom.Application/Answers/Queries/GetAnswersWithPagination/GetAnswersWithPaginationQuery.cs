using HUTECHClassroom.Application.Answers.DTOs;
using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Common.Requests;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Answers.Queries.GetAnswersWithPagination;

public record GetAnswersWithPaginationQuery(PaginationParams Params) : GetWithPaginationQuery<AnswerDTO>(Params);
public class GetAnswersWithPaginationQueryHandler : GetWithPaginationQueryHandler<Answer, GetAnswersWithPaginationQuery, AnswerDTO>
{
    public GetAnswersWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }
    protected override Expression<Func<Answer, bool>> SearchStringPredicate(string searchString) =>
        x => x.Description.ToLower().Contains(searchString.ToLower())
        || x.Link.ToLower().Contains(searchString.ToLower());
    protected override Expression<Func<Answer, object>> OrderByKeySelector()
    {
        return x => x.CreateDate;
    }
}

