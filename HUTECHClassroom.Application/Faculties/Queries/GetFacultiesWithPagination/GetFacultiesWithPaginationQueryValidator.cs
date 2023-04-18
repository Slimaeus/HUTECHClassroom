using HUTECHClassroom.Application.Common.Validators;
using HUTECHClassroom.Application.Faculties.DTOs;

namespace HUTECHClassroom.Application.Faculties.Queries.GetFacultiesWithPagination;

public class GetFacultiesWithPaginationQueryValidator : GetWithPaginationQueryValidator<GetFacultiesWithPaginationQuery, FacultyDTO>
{
    public GetFacultiesWithPaginationQueryValidator()
    {
    }
}
