﻿using EntityFrameworkCore.QueryBuilder.Interfaces;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Exercises.DTOs;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Classrooms.Queries.GetClassroomExercisesWithPagination;

public sealed record GetClassroomExercisesWithPaginationQuery(Guid Id, ClassroomPaginationParams Params) : GetWithPaginationQuery<ExerciseDTO, ClassroomPaginationParams>(Params);
public sealed class GetClassroomExercisesWithPaginationQueryHandler : GetWithPaginationQueryHandler<Exercise, GetClassroomExercisesWithPaginationQuery, ExerciseDTO, ClassroomPaginationParams>
{
    public GetClassroomExercisesWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
    protected override Expression<Func<Exercise, bool>> FilterPredicate(GetClassroomExercisesWithPaginationQuery query)
        => x => x.ClassroomId == query.Id && (query.Params.UserId == null || query.Params.UserId == Guid.Empty || x.ExerciseUsers.Any(eu => query.Params.UserId == eu.UserId));
    protected override Expression<Func<Exercise, bool>> SearchStringPredicate(string searchString)
    {
        var toLowerSearchString = searchString.ToLower();
        return x => x.Title.ToLower().Contains(toLowerSearchString)
        || (x.Instruction != null && x.Instruction.ToLower().Contains(toLowerSearchString))
        || (x.Topic != null && x.Topic.ToLower().Contains(toLowerSearchString));
    }
    protected override IQuery<Exercise> Order(IMultipleResultQuery<Exercise> query) 
    => query.OrderByDescending(x => x.CreateDate);

}
