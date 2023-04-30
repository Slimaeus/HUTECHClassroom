﻿using HUTECHClassroom.Application.Classrooms.DTOs;
using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Common.Requests;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Classrooms.Queries.GetClassroomExercisesWithPagination;

public record GetClassroomExercisesWithPaginationQuery(Guid Id, PaginationParams Params) : GetWithPaginationQuery<ClassroomExerciseDTO, PaginationParams>(Params);
public class GetClassroomExercisesWithPaginationQueryHandler : GetWithPaginationQueryHandler<Exercise, GetClassroomExercisesWithPaginationQuery, ClassroomExerciseDTO, PaginationParams>
{
    public GetClassroomExercisesWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
    protected override Expression<Func<Exercise, bool>> FilterPredicate(GetClassroomExercisesWithPaginationQuery query)
    {
        return x => x.ClassroomId == query.Id;
    }
    protected override Expression<Func<Exercise, bool>> SearchStringPredicate(string searchString)
    {
        var toLowerSearchString = searchString.ToLower();
        return x => x.Title.ToLower().Contains(toLowerSearchString)
        || x.Instruction.ToLower().Contains(toLowerSearchString)
        || x.Topic.ToLower().Contains(toLowerSearchString);
    }
    protected override Expression<Func<Exercise, object>> OrderByKeySelector()
    {
        return x => x.CreateDate;
    }
}
