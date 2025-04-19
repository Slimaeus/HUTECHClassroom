using EntityFrameworkCore.QueryBuilder.Interfaces;
using HUTECHClassroom.Application.Classrooms.DTOs;
using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Domain.Constants;
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
    {
        if (_userAccessor.Roles.Contains(RoleConstants.Dean))
        {
            return x => true;
        }
        else if (_userAccessor.Roles.Contains(RoleConstants.TrainingOffice))
        {
            return x => true;
        }
        else if (_userAccessor.Roles.Contains(RoleConstants.Lecturer))
        {
            return x => x.ClassroomUsers.Any(y => y.UserId == _userAccessor.Id) || x.LecturerId == _userAccessor.Id;
        }
        else if (_userAccessor.Roles.Contains(RoleConstants.Student))
        {
            return x => x.ClassroomUsers.Any(y => y.UserId == _userAccessor.Id) || x.LecturerId == _userAccessor.Id;
        }
        else if (_userAccessor.Roles.Contains(RoleConstants.DepartmentSecretary))
        {
            return x => true;
        }
        else if (_userAccessor.Roles.Contains(RoleConstants.Administrator))
        {
            return x => true;
        }

        return x => false;
    }

    protected override Expression<Func<Classroom, bool>> SearchStringPredicate(string searchString)
    {
        var toLowerSearchString = searchString.ToLower();
        return x => (x.Subject != null && x.Subject.Title.ToLower().Contains(toLowerSearchString)) || (x.Description != null && x.Description.ToLower().Contains(toLowerSearchString)) || (x.Class != null && x.Class.Name.ToLower().Contains(toLowerSearchString));
    }
}
