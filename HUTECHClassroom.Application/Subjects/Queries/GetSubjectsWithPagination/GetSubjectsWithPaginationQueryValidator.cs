using HUTECHClassroom.Application.Common.Validators;
using HUTECHClassroom.Application.Subjects.DTOs;

namespace HUTECHClassroom.Application.Subjects.Queries.GetSubjectsWithPagination;

public class GetSubjectsWithPaginationQueryValidator : GetWithPaginationQueryValidator<GetSubjectsWithPaginationQuery, SubjectDTO, SubjectPaginationParams> { }
