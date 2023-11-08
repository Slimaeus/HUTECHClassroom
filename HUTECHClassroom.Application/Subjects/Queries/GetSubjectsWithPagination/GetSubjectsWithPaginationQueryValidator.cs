using HUTECHClassroom.Application.Common.Validators;
using HUTECHClassroom.Application.Subjects.DTOs;

namespace HUTECHClassroom.Application.Subjects.Queries.GetSubjectsWithPagination;

public sealed class GetSubjectsWithPaginationQueryValidator : GetWithPaginationQueryValidator<GetSubjectsWithPaginationQuery, SubjectDTO, SubjectPaginationParams> { }
