using HUTECHClassroom.Application.Classrooms.DTOs;
using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Common.Requests;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Classrooms.Queries.GetClassroomsWithPagination;

public record GetClassroomsWithPaginationQuery(PaginationParams Params) : GetWithPaginationQuery<ClassroomDTO, PaginationParams>(Params);
public class GetClassroomsWithPaginationQueryHandler : GetWithPaginationQueryHandler<Classroom, GetClassroomsWithPaginationQuery, ClassroomDTO, PaginationParams>
{
    public GetClassroomsWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
    protected override Expression<Func<Classroom, bool>> SearchStringPredicate(string searchString)
    {
        var toLowerSearchString = searchString.ToLower();
        return x => x.Title.ToLower().Contains(toLowerSearchString) || x.Description.ToLower().Contains(toLowerSearchString);
    }
    protected override Expression<Func<Classroom, object>> OrderByKeySelector()
    {
        return x => x.CreateDate;
    }
}
