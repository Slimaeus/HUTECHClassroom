﻿namespace HUTECHClassroom.Application.Common.Validators.Classrooms;

public sealed class ClassroomExistenceByIdValidator : EntityExistenceByIdValidator<Classroom>
{
    public ClassroomExistenceByIdValidator(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
}
