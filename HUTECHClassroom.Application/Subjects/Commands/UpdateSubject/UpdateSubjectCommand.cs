﻿using HUTECHClassroom.Application.Common.Requests;

namespace HUTECHClassroom.Application.Subjects.Commands.UpdateSubject;

public sealed record UpdateSubjectCommand(Guid Id) : UpdateCommand(Id)
{
    public string? Code { get; set; }
    public string? Title { get; set; }
    public int? TotalCredits { get; set; }
}
public sealed class UpdateSubjectCommandHandler : UpdateCommandHandler<Subject, UpdateSubjectCommand>
{
    public UpdateSubjectCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}
