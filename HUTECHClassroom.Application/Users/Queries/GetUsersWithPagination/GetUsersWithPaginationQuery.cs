using EntityFrameworkCore.QueryBuilder.Interfaces;
using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Users.DTOs;
using HUTECHClassroom.Domain.Constants;
using HUTECHClassroom.Domain.Interfaces;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Users.Queries.GetUsersWithPagination;

public sealed record GetUsersWithPaginationQuery(UserPaginationParams Params) : GetWithPaginationQuery<UserDTO, UserPaginationParams>(Params);
public sealed class GetUsersWithPaginationQueryHandler : GetWithPaginationQueryHandler<ApplicationUser, GetUsersWithPaginationQuery, UserDTO, UserPaginationParams>
{
    private readonly IUserAccessor _userAccessor;

    public GetUsersWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserAccessor userAccessor) : base(unitOfWork, mapper)
    {
        _userAccessor = userAccessor;
    }
    protected override Expression<Func<ApplicationUser, bool>> SearchStringPredicate(string searchString)
        => x => x.FirstName.ToLower().Contains(searchString.ToLower())
        || x.LastName.ToLower().Contains(searchString.ToLower())
        || (x.Email != null && x.Email.ToLower().Contains(searchString.ToLower()));

    protected override Expression<Func<ApplicationUser, bool>> FilterPredicate(GetUsersWithPaginationQuery query)
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
    protected override IQuery<ApplicationUser> Order(IMultipleResultQuery<ApplicationUser> query)
        => query.OrderByDescending(x => x.FirstName);
}

