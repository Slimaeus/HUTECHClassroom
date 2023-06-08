using EntityFrameworkCore.QueryBuilder.Interfaces;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Groups.DTOs;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Classrooms.Queries.GetClassroomGroupsWithPagination;

public record GetClassroomGroupsWithPaginationQuery(Guid Id, ClassroomPaginationParams Params) : GetWithPaginationQuery<GroupDTO, ClassroomPaginationParams>(Params);
public class GetClassroomGroupsWithPaginationQueryHandler : GetWithPaginationQueryHandler<Group, GetClassroomGroupsWithPaginationQuery, GroupDTO, ClassroomPaginationParams>
{
    public GetClassroomGroupsWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
    protected override Expression<Func<Group, bool>> FilterPredicate(GetClassroomGroupsWithPaginationQuery query)
    {
        return x => x.ClassroomId == query.Id && (query.Params.UserId == Guid.Empty || x.GroupUsers.Any(gu => query.Params.UserId == gu.UserId));
    }
    protected override Expression<Func<Group, bool>> SearchStringPredicate(string searchString)
    {
        var toLowerSearchString = searchString.ToLower();
        return x => x.Name.ToLower().Contains(toLowerSearchString) || x.Description.ToLower().Contains(toLowerSearchString);
    }
    protected override IQuery<Group> Order(IMultipleResultQuery<Group> query) => query.OrderByDescending(x => x.CreateDate);

}
