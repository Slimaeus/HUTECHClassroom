using EntityFrameworkCore.QueryBuilder.Interfaces;
using HUTECHClassroom.Application.Classrooms.DTOs;
using HUTECHClassroom.Application.Common.Extensions;
using HUTECHClassroom.Application.Common.Requests;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Classrooms.Queries.GetClassroomsWithPagination;

public record GetClassroomsWithPaginationQuery(ClassroomPaginationParams Params) : GetWithPaginationQuery<ClassroomDTO, ClassroomPaginationParams>(Params);
public class GetClassroomsWithPaginationQueryHandler : GetWithPaginationQueryHandler<Classroom, GetClassroomsWithPaginationQuery, ClassroomDTO, ClassroomPaginationParams>
{
    public GetClassroomsWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
    protected override Expression<Func<Classroom, bool>> SearchStringPredicate(string searchString)
    {
        var toLowerSearchString = searchString.ToLower();
        return x => x.Title.ToLower().Contains(toLowerSearchString) || x.Description.ToLower().Contains(toLowerSearchString);
    }
    protected override Expression<Func<Classroom, object>> OrderByKeySelector() => x => x.CreateDate;
    protected override IMultipleResultQuery<Classroom> SortingQuery(IMultipleResultQuery<Classroom> query, GetClassroomsWithPaginationQuery request)
        => query.SortEntityQuery(request.Params.TitleOrder, x => x.Title)
                .SortEntityQuery(request.Params.DescriptionOrder, x => x.Description)
                .SortEntityQuery(request.Params.RoomOrder, x => x.Room)
                .SortEntityQuery(request.Params.TopicOrder, x => x.Topic);
}
