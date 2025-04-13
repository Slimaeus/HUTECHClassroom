using EntityFrameworkCore.QueryBuilder.Interfaces;
using HUTECHClassroom.Application.Classes.DTOs;
using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Domain.Constants;
using HUTECHClassroom.Domain.Interfaces;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Users.Queries.GetUserClassesWithPagination;

public sealed record GetUserClassesWithPaginationQuery(PaginationParams Params) : GetWithPaginationQuery<ClassDTO, PaginationParams>(Params);
public sealed class GetUserClassesWithPaginationQueryHandler : GetWithPaginationQueryHandler<Class, GetUserClassesWithPaginationQuery, ClassDTO, PaginationParams>
{
    private readonly IUserAccessor _userAccessor;

    public GetUserClassesWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserAccessor userAccessor) : base(unitOfWork, mapper)
        => _userAccessor = userAccessor;
    protected override IQuery<Class> Order(IMultipleResultQuery<Class> query)
        => query.OrderByDescending(x => x.CreateDate);
    protected override Expression<Func<Class, bool>> FilterPredicate(GetUserClassesWithPaginationQuery query)
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
            return x => x.Classrooms.Any(c => c.LecturerId == _userAccessor.Id);
        }
        else if (_userAccessor.Roles.Contains(RoleConstants.Student))
        {
            return x => x.Classrooms.Any(y => y.ClassroomUsers.Any(cu => cu.UserId == _userAccessor.Id));
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

    protected override Expression<Func<Class, bool>> SearchStringPredicate(string searchString)
    {
        var toLowerSearchString = searchString.ToLower();
        return x => x.Name.ToLower().Contains(toLowerSearchString);
    }
}
