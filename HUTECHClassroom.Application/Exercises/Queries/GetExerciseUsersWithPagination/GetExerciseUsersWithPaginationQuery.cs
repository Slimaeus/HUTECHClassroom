using HUTECHClassroom.Application.Common.DTOs;
using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Common.Requests;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Exercises.Queries.GetExerciseUsersWithPagination;

public record GetExerciseUsersWithPaginationQuery(Guid Id, PaginationParams Params) : GetWithPaginationQuery<MemberDTO>(Params);
public class GetExerciseUsersWithPaginationQueryHandler : GetWithPaginationQueryHandler<ApplicationUser, GetExerciseUsersWithPaginationQuery, MemberDTO>
{
    public GetExerciseUsersWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
    protected override Expression<Func<ApplicationUser, bool>> FilterPredicate(GetExerciseUsersWithPaginationQuery query)
    {
        return x => x.ExerciseUsers.Any(x => x.ExerciseId == query.Id);
    }
    protected override Expression<Func<ApplicationUser, bool>> SearchStringPredicate(string searchString)
    {
        var toLowerSearchString = searchString.ToLower();
        return x => x.UserName.ToLower().Contains(toLowerSearchString) || x.Email.ToLower().Contains(toLowerSearchString);
    }
    protected override Expression<Func<ApplicationUser, object>> OrderByKeySelector()
    {
        return x => x.UserName;
    }
}
