using HUTECHClassroom.Application.Common.Validators;
using HUTECHClassroom.Application.Faculties.DTOs;

namespace HUTECHClassroom.Application.Faculties.Queries.GetFacultiesWithPagination;

public sealed class GetFacultiesWithPaginationQueryValidator : GetWithPaginationQueryValidator<GetFacultiesWithPaginationQuery, FacultyDTO, FacultyPaginationParams> { }
