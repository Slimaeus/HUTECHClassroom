using EntityFrameworkCore.QueryBuilder.Interfaces;
using HUTECHClassroom.Application.Common.Extensions;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Subjects.DTOs;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Subjects.Queries.GetSubjectsWithPagination;

public record GetSubjectsWithPaginationQuery(SubjectPaginationParams Params) : GetWithPaginationQuery<SubjectDTO, SubjectPaginationParams>(Params);
public class GetSubjectsWithPaginationQueryHandler : GetWithPaginationQueryHandler<Subject, GetSubjectsWithPaginationQuery, SubjectDTO, SubjectPaginationParams>
{
    public GetSubjectsWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }
    protected override Expression<Func<Subject, bool>> SearchStringPredicate(string searchString)
        => x => x.Code.ToLower().Contains(searchString.ToLower()) || x.Title.ToLower().Contains(searchString.ToLower());

    protected override Expression<Func<Subject, object>> OrderByKeySelector()
        => x => x.CreateDate;
    protected override IMultipleResultQuery<Subject> SortingQuery(IMultipleResultQuery<Subject> query, GetSubjectsWithPaginationQuery request)
        => query.SortEntityQuery(request.Params.CodeOrder, x => x.Code)
                .SortEntityQuery(request.Params.TitleOrder, x => x.Title)
                .SortEntityQuery(request.Params.TotalCreditsOrder, x => x.TotalCredits);
}

