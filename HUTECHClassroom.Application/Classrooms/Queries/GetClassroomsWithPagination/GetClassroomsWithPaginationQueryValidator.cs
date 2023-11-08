using HUTECHClassroom.Application.Classrooms.DTOs;
using HUTECHClassroom.Application.Common.Validators;

namespace HUTECHClassroom.Application.Classrooms.Queries.GetClassroomsWithPagination;

public sealed class GetClassroomsWithPaginationQueryValidator : GetWithPaginationQueryValidator<GetClassroomsWithPaginationQuery, ClassroomDTO, ClassroomPaginationParams>
{
}
