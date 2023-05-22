using HUTECHClassroom.Application.Classrooms.DTOs;
using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Common.Validators;

namespace HUTECHClassroom.Application.Users.Queries.GetUserClassroomsWithPagination;

public class GetUserClassroomsWithPaginationQueryValidator : GetWithPaginationQueryValidator<GetUserClassroomsWithPaginationQuery, ClassroomDTO, PaginationParams>
{
}
