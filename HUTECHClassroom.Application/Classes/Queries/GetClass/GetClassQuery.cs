using HUTECHClassroom.Application.Classes.DTOs;
using HUTECHClassroom.Application.Common.Requests;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Classes.Queries.GetClass;

public record GetClassQuery(Guid Id) : GetQuery<ClassDTO>;
public class GetClassQueryHandler : GetQueryHandler<Class, GetClassQuery, ClassDTO>
{
    public GetClassQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
    public override Expression<Func<Class, bool>> FilterPredicate(GetClassQuery query)
    {
        return x => x.Id == query.Id;
    }
    public override object GetNotFoundKey(GetClassQuery query)
    {
        return query.Id;
    }
}
