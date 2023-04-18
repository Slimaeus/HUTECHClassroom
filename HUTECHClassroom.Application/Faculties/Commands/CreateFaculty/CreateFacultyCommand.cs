using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Faculties.DTOs;
using HUTECHClassroom.Domain.Entities;

namespace HUTECHClassroom.Application.Faculties.Commands.CreateFaculty;

public record CreateFacultyCommand : CreateCommand<FacultyDTO>
{
    public string Name { get; set; }
}
public class CreateFacultyCommandHandler : CreateCommandHandler<Faculty, CreateFacultyCommand, FacultyDTO>
{
    public CreateFacultyCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}
