using HUTECHClassroom.Application.Common.Validators;
using HUTECHClassroom.Application.Subjects.DTOs;

namespace HUTECHClassroom.Application.Subjects.Commands.DeleteSubject;

public sealed class DeleteSubjectCommandValidator : DeleteCommandValidator<DeleteSubjectCommand, SubjectDTO>
{
}
