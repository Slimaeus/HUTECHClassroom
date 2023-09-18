using EntityFrameworkCore.QueryBuilder.Interfaces;
using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Exercises.DTOs;
using HUTECHClassroom.Domain.Interfaces;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Users.Queries.GetUserExercisesWithPagination;

public record GetUserExercisesWithPaginationQuery(PaginationParams Params) : GetWithPaginationQuery<ExerciseDTO, PaginationParams>(Params);
public class GetUserExercisesWithPaginationQueryHandler : GetWithPaginationQueryHandler<Exercise, GetUserExercisesWithPaginationQuery, ExerciseDTO, PaginationParams>
{
    private readonly IUserAccessor _userAccessor;

    public GetUserExercisesWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserAccessor userAccessor) : base(unitOfWork, mapper)
        => _userAccessor = userAccessor;
    protected override IQuery<Exercise> Order(IMultipleResultQuery<Exercise> query)
        => query.OrderByDescending(x => x.CreateDate);
    protected override Expression<Func<Exercise, bool>> FilterPredicate(GetUserExercisesWithPaginationQuery query)
        => x => x.ExerciseUsers.Any(x => x.UserId == _userAccessor.Id);
}
