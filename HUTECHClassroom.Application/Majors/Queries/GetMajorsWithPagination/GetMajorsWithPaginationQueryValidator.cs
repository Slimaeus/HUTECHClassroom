using HUTECHClassroom.Application.Common.Validators;
using HUTECHClassroom.Application.Majors.DTOs;

namespace HUTECHClassroom.Application.Majors.Queries.GetMajorsWithPagination;

public class GetMajorsWithPaginationQueryValidator : GetWithPaginationQueryValidator<GetMajorsWithPaginationQuery, MajorDTO, MajorPaginationParams> { }
