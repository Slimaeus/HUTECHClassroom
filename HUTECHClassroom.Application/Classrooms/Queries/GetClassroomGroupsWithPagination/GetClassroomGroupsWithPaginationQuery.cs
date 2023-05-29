using EntityFrameworkCore.QueryBuilder.Interfaces;
using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Groups.DTOs;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Classrooms.Queries.GetClassroomGroupsWithPagination;

public record GetClassroomGroupsWithPaginationQuery(Guid Id, PaginationParams Params) : GetWithPaginationQuery<GroupDTO, PaginationParams>(Params);
public class GetClassroomGroupsWithPaginationQueryHandler : GetWithPaginationQueryHandler<Group, GetClassroomGroupsWithPaginationQuery, GroupDTO, PaginationParams>
{
    public GetClassroomGroupsWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
    protected override Expression<Func<Group, bool>> FilterPredicate(GetClassroomGroupsWithPaginationQuery query)
    {
        return x => x.ClassroomId == query.Id;
    }
    protected override Expression<Func<Group, bool>> SearchStringPredicate(string searchString)
    {
        var toLowerSearchString = searchString.ToLower();
        return x => x.Name.ToLower().Contains(toLowerSearchString) || x.Description.ToLower().Contains(toLowerSearchString);
    }
    protected override IQuery<Group> Order(IMultipleResultQuery<Group> query) => query.OrderByDescending(x => x.CreateDate);

}
