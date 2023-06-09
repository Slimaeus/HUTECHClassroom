using EntityFrameworkCore.QueryBuilder.Interfaces;
using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Exercises.DTOs;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Exercises.Queries.GetExerciseUsersWithPagination;

public record GetExerciseUsersWithPaginationQuery(Guid Id, PaginationParams Params) : GetWithPaginationQuery<ExerciseUserDTO, PaginationParams>(Params);
public class GetExerciseUsersWithPaginationQueryHandler : GetWithPaginationQueryHandler<ExerciseUser, GetExerciseUsersWithPaginationQuery, ExerciseUserDTO, PaginationParams>
{
    public GetExerciseUsersWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
    protected override Expression<Func<ExerciseUser, bool>> FilterPredicate(GetExerciseUsersWithPaginationQuery query)
    {
        return x => x.ExerciseId == query.Id;
    }
    protected override Expression<Func<ExerciseUser, bool>> SearchStringPredicate(string searchString)
    {
        var toLowerSearchString = searchString.ToLower();
        return x => x.User.UserName.ToLower().Contains(toLowerSearchString) || x.User.Email.ToLower().Contains(toLowerSearchString);
    }
    protected override IQuery<ExerciseUser> Order(IMultipleResultQuery<ExerciseUser> query) => query.OrderBy(x => x.User.UserName);

}
