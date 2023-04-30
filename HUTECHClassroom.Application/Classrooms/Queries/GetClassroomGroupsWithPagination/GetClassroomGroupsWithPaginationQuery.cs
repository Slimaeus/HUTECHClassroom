using HUTECHClassroom.Application.Classrooms.DTOs;
using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Common.Requests;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Classrooms.Queries.GetClassroomGroupsWithPagination;

public record GetClassroomGroupsWithPaginationQuery(Guid Id, PaginationParams Params) : GetWithPaginationQuery<ClassroomGroupDTO, PaginationParams>(Params);
public class GetClassroomGroupsWithPaginationQueryHandler : GetWithPaginationQueryHandler<Group, GetClassroomGroupsWithPaginationQuery, ClassroomGroupDTO, PaginationParams>
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
    protected override Expression<Func<Group, object>> OrderByKeySelector()
    {
        return x => x.CreateDate;
    }
}
