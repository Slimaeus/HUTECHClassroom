using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Majors.DTOs;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Majors.Queries.GetMajor;

public record GetMajorQuery(Guid Id) : GetQuery<MajorDTO>;
public sealed class GetMajorQueryHandler : GetQueryHandler<Major, GetMajorQuery, MajorDTO>
{
    public GetMajorQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }
    public override Expression<Func<Major, bool>> FilterPredicate(GetMajorQuery query)
    {
        return x => x.Id == query.Id;
    }
    public override object GetNotFoundKey(GetMajorQuery query)
    {
        return query.Id;
    }
}
