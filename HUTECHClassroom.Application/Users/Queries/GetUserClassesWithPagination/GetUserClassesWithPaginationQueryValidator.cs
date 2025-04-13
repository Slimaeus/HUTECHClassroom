using HUTECHClassroom.Application.Classes.DTOs;
using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Common.Validators;
using HUTECHClassroom.Application.Users.Queries.GetUserClassesWithPagination;

namespace HUTECHClassroom.Application.Users.Queries.GetUserClassroomsWithPagination;

public sealed class GetUserClassesWithPaginationQueryValidator : GetWithPaginationQueryValidator<GetUserClassesWithPaginationQuery, ClassDTO, PaginationParams>
{
}
