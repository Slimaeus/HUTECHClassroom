﻿using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using HUTECHClassroom.Application.Classrooms.DTOs;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Domain.Entities;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Classrooms.Queries.GetClassroom;

public record GetClassroomQuery(Guid Id) : GetQuery<ClassroomDTO>;
public class GetClassroomQueryHandler : GetQueryHandler<Classroom, GetClassroomQuery, ClassroomDTO>
{
    public GetClassroomQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
    public override Expression<Func<Classroom, bool>> FilterPredicate(GetClassroomQuery query)
    {
        return x => x.Id == query.Id;
    }
    public override object GetNotFoundKey(GetClassroomQuery query)
    {
        return query.Id;
    }
}
