using EntityFrameworkCore.QueryBuilder.Interfaces;
using HUTECHClassroom.Application.Classrooms.DTOs;
using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Domain.Interfaces;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Users.Queries.GetUserClassroomsWithPagination;

public sealed record GetUserClassroomsWithPaginationQuery(PaginationParams Params) : GetWithPaginationQuery<ClassroomDTO, PaginationParams>(Params);
public sealed class GetUserClassroomsWithPaginationQueryHandler : GetWithPaginationQueryHandler<Classroom, GetUserClassroomsWithPaginationQuery, ClassroomDTO, PaginationParams>
{
    private readonly IUserAccessor _userAccessor;

    public GetUserClassroomsWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserAccessor userAccessor) : base(unitOfWork, mapper)
        => _userAccessor = userAccessor;
    protected override IQuery<Classroom> Order(IMultipleResultQuery<Classroom> query)
        => query.OrderByDescending(x => x.CreateDate);
    protected override Expression<Func<Classroom, bool>> FilterPredicate(GetUserClassroomsWithPaginationQuery query)
        => x => x.ClassroomUsers.Any(y => y.UserId == _userAccessor.Id) || x.LecturerId == _userAccessor.Id;
}
