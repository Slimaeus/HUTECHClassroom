using EntityFrameworkCore.QueryBuilder.Interfaces;
using HUTECHClassroom.Application.Classrooms.DTOs;
using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Common.Requests;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Classrooms.Queries.GetClassroomUsersWithPagination;

public record GetClassroomUsersWithPaginationQuery(Guid Id, PaginationParams Params) : GetWithPaginationQuery<ClassroomUserDTO, PaginationParams>(Params);
public class GetClassroomUsersWithPaginationQueryHandler : GetWithPaginationQueryHandler<ClassroomUser, GetClassroomUsersWithPaginationQuery, ClassroomUserDTO, PaginationParams>
{
    public GetClassroomUsersWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
    protected override Expression<Func<ClassroomUser, bool>> FilterPredicate(GetClassroomUsersWithPaginationQuery query)
    {
        return x => x.ClassroomId == query.Id;
    }
    protected override Expression<Func<ClassroomUser, bool>> SearchStringPredicate(string searchString)
    {
        var toLowerSearchString = searchString.ToLower();
        return x => x.User.UserName.ToLower().Contains(toLowerSearchString) || x.User.Email.ToLower().Contains(toLowerSearchString);
    }
    protected override IQuery<ClassroomUser> Order(IMultipleResultQuery<ClassroomUser> query) => query.OrderBy(x => x.User.UserName);

}
