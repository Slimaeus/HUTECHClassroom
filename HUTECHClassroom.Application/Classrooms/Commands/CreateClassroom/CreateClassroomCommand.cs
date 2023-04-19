using AutoMapper;
using EntityFrameworkCore.Repository.Interfaces;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using HUTECHClassroom.Application.Classrooms.DTOs;
using HUTECHClassroom.Application.Common.Exceptions;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Domain.Entities;

namespace HUTECHClassroom.Application.Classrooms.Commands.CreateClassroom;

public record CreateClassroomCommand : CreateCommand<ClassroomDTO>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Topic { get; set; }
    public Guid FacultyId { get; set; }
    public string LecturerName { get; set; }
}
public class CreateClassroomCommandHandler : CreateCommandHandler<Classroom, CreateClassroomCommand, ClassroomDTO>
{
    private readonly IRepository<ApplicationUser> _userRepository;
    private readonly IRepository<Faculty> _facultyRepository;

    public CreateClassroomCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
        _userRepository = unitOfWork.Repository<ApplicationUser>();
        _facultyRepository = unitOfWork.Repository<Faculty>();
    }
    protected override async Task ValidateAdditionalField(CreateClassroomCommand request, Classroom entity)
    {
        var userQuery = _userRepository
            .SingleResultQuery()
            .AndFilter(x => x.UserName == request.LecturerName);

        var lecturer = await _userRepository.SingleOrDefaultAsync(userQuery);

        if (lecturer == null) throw new NotFoundException(nameof(ApplicationUser), request.LecturerName);

        entity.Lecturer = lecturer;

        var facultyQuery = _facultyRepository
            .SingleResultQuery()
            .AndFilter(x => x.Id == request.FacultyId);

        var faculty = await _facultyRepository.SingleOrDefaultAsync(facultyQuery);

        if (faculty == null) throw new NotFoundException(nameof(Faculty), request.FacultyId);

        entity.Faculty = faculty;
    }
}
