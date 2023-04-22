using HUTECHClassroom.Application.Answers.DTOs;
using HUTECHClassroom.Application.Common.Requests;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Answers.Queries.GetAnswer;

public record GetAnswerQuery(Guid Id) : GetQuery<AnswerDTO>;
public class GetAnswerQueryHandler : GetQueryHandler<Answer, GetAnswerQuery, AnswerDTO>
{
    public GetAnswerQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }
    public override Expression<Func<Answer, bool>> FilterPredicate(GetAnswerQuery query)
    {
        return x => x.Id == query.Id;
    }
    public override object GetNotFoundKey(GetAnswerQuery query)
    {
        return query.Id;
    }
}
