﻿using HUTECHClassroom.Application.Common.Requests;

namespace HUTECHClassroom.Application.Faculties.Commands.UpdateFaculty;

public record UpdateFacultyCommand(Guid Id) : UpdateCommand(Id)
{
    public string Name { get; set; }
}
public class UpdateFacultyCommandHandler : UpdateCommandHandler<Faculty, UpdateFacultyCommand>
{
    public UpdateFacultyCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}
