using HUTECHClassroom.Application.Common.Validators;
using HUTECHClassroom.Application.Majors.DTOs;

namespace HUTECHClassroom.Application.Majors.Commands.DeleteMajor;

public class DeleteMajorCommandValidator : DeleteCommandValidator<string, DeleteMajorCommand, MajorDTO>
{
}
