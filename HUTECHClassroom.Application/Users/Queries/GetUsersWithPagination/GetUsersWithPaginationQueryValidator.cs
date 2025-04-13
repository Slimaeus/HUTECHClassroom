using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Common.Validators;
using HUTECHClassroom.Application.Users.DTOs;

namespace HUTECHClassroom.Application.Users.Queries.GetUsersWithPagination;

public sealed class GetUsersWithPaginationQueryValidator : GetWithPaginationQueryValidator<GetUsersWithPaginationQuery, UserDTO, UserPaginationParams> { }
