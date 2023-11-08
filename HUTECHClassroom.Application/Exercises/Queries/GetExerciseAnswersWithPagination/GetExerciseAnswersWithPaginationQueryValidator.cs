using HUTECHClassroom.Application.Answers.DTOs;
using HUTECHClassroom.Application.Common.Validators;

namespace HUTECHClassroom.Application.Exercises.Queries.GetExerciseAnswersWithPagination;

public sealed class GetExerciseAnswersWithPaginationQueryValidator : GetWithPaginationQueryValidator<GetExerciseAnswersWithPaginationQuery, AnswerDTO, ExercisePaginationParams>
{ }
