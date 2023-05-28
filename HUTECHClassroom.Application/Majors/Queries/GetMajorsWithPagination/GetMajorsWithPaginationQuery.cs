using EntityFrameworkCore.QueryBuilder.Interfaces;
using HUTECHClassroom.Application.Common.Extensions;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Majors.DTOs;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Majors.Queries.GetMajorsWithPagination;

public record GetMajorsWithPaginationQuery(MajorPaginationParams Params) : GetWithPaginationQuery<MajorDTO, MajorPaginationParams>(Params);
public class GetMajorsWithPaginationQueryHandler : GetWithPaginationQueryHandler<Major, GetMajorsWithPaginationQuery, MajorDTO, MajorPaginationParams>
{
    public GetMajorsWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }
    protected override Expression<Func<Major, bool>> SearchStringPredicate(string searchString)
        => x => x.Code.ToLower().Contains(searchString.ToLower()) || x.Title.ToLower().Contains(searchString.ToLower());

    protected override IQuery<Major> Order(IMultipleResultQuery<Major> query) => query.OrderByDescending(x => x.CreateDate);

    protected override IMultipleResultQuery<Major> SortingQuery(IMultipleResultQuery<Major> query, GetMajorsWithPaginationQuery request)
        => query.SortEntityQuery(request.Params.CodeOrder, x => x.Code)
                .SortEntityQuery(request.Params.TitleOrder, x => x.Title)
                .SortEntityQuery(request.Params.TotalCreditsOrder, x => x.TotalCredits)
                .SortEntityQuery(request.Params.NonComulativeCreditsOrder, x => x.NonComulativeCredits);
}

