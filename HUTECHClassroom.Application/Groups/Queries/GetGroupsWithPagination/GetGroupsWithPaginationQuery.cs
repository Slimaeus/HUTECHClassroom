using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Groups.DTOs;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Groups.Queries.GetGroupsWithPagination;

public record GetGroupsWithPaginationQuery(PaginationParams Params) : GetWithPaginationQuery<GroupDTO, PaginationParams>(Params);
public class GetGroupsWithPaginationQueryHandler : GetWithPaginationQueryHandler<Group, GetGroupsWithPaginationQuery, GroupDTO, PaginationParams>
{
    public GetGroupsWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
    protected override Expression<Func<Group, bool>> SearchStringPredicate(string searchString)
    {
        return x => x.Name.ToLower().Equals(searchString.ToLower()) || x.Description.ToLower().Equals(searchString.ToLower());
    }
    protected override Expression<Func<Group, object>> OrderByKeySelector()
    {
        return x => x.CreateDate;
    }
}
