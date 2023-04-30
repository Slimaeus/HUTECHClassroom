using HUTECHClassroom.Application.Classrooms.DTOs;
using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Common.Validators;

namespace HUTECHClassroom.Application.Classrooms.Queries.GetClassroomsWithPagination;

public class GetClassroomsWithPaginationQueryValidator : GetWithPaginationQueryValidator<GetClassroomsWithPaginationQuery, ClassroomDTO, PaginationParams>
{
}
