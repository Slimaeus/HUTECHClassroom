using HUTECHClassroom.Application.Common.DTOs;

namespace HUTECHClassroom.Application.Exercises.DTOs;

public record ExerciseUserDTO(Guid Id, string UserName, string Email, string FirstName, string LastName, bool IsSubmmited) : IEntityDTO { }
