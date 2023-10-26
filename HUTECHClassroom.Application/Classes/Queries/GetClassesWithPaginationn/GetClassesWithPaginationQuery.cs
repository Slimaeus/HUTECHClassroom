using EntityFrameworkCore.QueryBuilder.Interfaces;
using HUTECHClassroom.Application.Classes.DTOs;
using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Common.Requests;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Classes.Queries.GetClassesWithPaginationn;

public sealed record GetClassesWithPaginationQuery(PaginationParams Params) : GetWithPaginationQuery<ClassDTO, PaginationParams>(Params);
public sealed class GetClassesWithPaginationQueryHandler : GetWithPaginationQueryHandler<Class, GetClassesWithPaginationQuery, ClassDTO, PaginationParams>
{
    public GetClassesWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
    protected override Expression<Func<Class, bool>> SearchStringPredicate(string searchString)
    {
        var toLowerSearchString = searchString.ToLower();
        return x => x.Name.ToLower().Contains(toLowerSearchString);
    }
    protected override IQuery<Class> Order(IMultipleResultQuery<Class> query) => query.OrderByDescending(x => x.CreateDate);

}
