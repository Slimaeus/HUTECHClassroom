using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Subjects.DTOs;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Subjects.Queries.GetSubject;

public record GetSubjectQuery(Guid Id) : GetQuery<SubjectDTO>;
public sealed class GetSubjectQueryHandler : GetQueryHandler<Subject, GetSubjectQuery, SubjectDTO>
{
    public GetSubjectQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }
    public override Expression<Func<Subject, bool>> FilterPredicate(GetSubjectQuery query)
    {
        return x => x.Id == query.Id;
    }
    public override object GetNotFoundKey(GetSubjectQuery query)
    {
        return query.Id;
    }
}
