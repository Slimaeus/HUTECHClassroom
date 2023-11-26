using EntityFrameworkCore.QueryBuilder.Interfaces;
using HUTECHClassroom.Application.Classrooms.DTOs;
using HUTECHClassroom.Application.Common.Extensions;
using HUTECHClassroom.Application.Common.Requests;
using System.Linq.Expressions;

namespace HUTECHClassroom.Application.Classrooms.Queries.GetClassroomsWithPagination;

public sealed record GetClassroomsWithPaginationQuery(ClassroomPaginationParams Params) : GetWithPaginationQuery<ClassroomDTO, ClassroomPaginationParams>(Params);
public sealed class GetClassroomsWithPaginationQueryHandler : GetWithPaginationQueryHandler<Classroom, GetClassroomsWithPaginationQuery, ClassroomDTO, ClassroomPaginationParams>
{
    public GetClassroomsWithPaginationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
    protected override Expression<Func<Classroom, bool>> FilterPredicate(GetClassroomsWithPaginationQuery query)
        => x => query.Params.UserId == null || query.Params.UserId == Guid.Empty || x.LecturerId == query.Params.UserId || x.ClassroomUsers.Any(cu => cu.UserId == query.Params.UserId);
    protected override Expression<Func<Classroom, bool>> SearchStringPredicate(string searchString)
    {
        var toLowerSearchString = searchString.ToLower();
        return x => x.Title.ToLower().Contains(toLowerSearchString) || (x.Description != null && x.Description.ToLower().Contains(toLowerSearchString)) || (x.Class != null && x.Class.Name.ToLower().Contains(toLowerSearchString));
    }
    protected override IQuery<Classroom> Order(IMultipleResultQuery<Classroom> query)
        => query.OrderByDescending(x => x.CreateDate);

    protected override IMultipleResultQuery<Classroom> SortingQuery(IMultipleResultQuery<Classroom> query, GetClassroomsWithPaginationQuery request)
        => query.SortEntityQuery(request.Params.TitleOrder, x => x.Title)
                .SortEntityQuery(request.Params.DescriptionOrder, x => x.Description)
                .SortEntityQuery(request.Params.RoomOrder, x => x.Room)
                .SortEntityQuery(request.Params.TopicOrder, x => x.Topic)
                .SortEntityQuery(request.Params.StudyPeriodOrder, x => x.StudyPeriod)
                .SortEntityQuery(request.Params.ClassOrder, x => x.Class)
                .SortEntityQuery(request.Params.SchoolYearOrder, x => x.SchoolYear)
                .SortEntityQuery(request.Params.StudyGroupOrder, x => x.StudyGroup)
                .SortEntityQuery(request.Params.PracticalStudyGroupOrder, x => x.PracticalStudyGroup)
                .SortEntityQuery(request.Params.SemesterOrder, x => x.Semester)
                .SortEntityQuery(request.Params.TypeOrder, x => x.Type);
}
