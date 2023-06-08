using HUTECHClassroom.Application.Answers.DTOs;
using HUTECHClassroom.Application.Common.Models;
using HUTECHClassroom.Application.Common.Validators;

namespace HUTECHClassroom.Application.Exercises.Queries.GetExerciseAnswersWithPagination;

public class GetExerciseAnswersWithPaginationQueryValidator : GetWithPaginationQueryValidator<GetExerciseAnswersWithPaginationQuery, AnswerDTO, PaginationParams>
{ }
