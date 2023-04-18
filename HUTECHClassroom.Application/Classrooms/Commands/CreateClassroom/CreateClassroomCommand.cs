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
    public string LecturerName { get; set; }
}
public class CreateClassroomCommandHandler : CreateCommandHandler<Classroom, CreateClassroomCommand, ClassroomDTO>
{
    private IRepository<ApplicationUser> _userRepository;

    public CreateClassroomCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
        _userRepository = unitOfWork.Repository<ApplicationUser>();
    }
    protected override async Task ValidateAdditionalField(CreateClassroomCommand request, Classroom entity)
    {
        var query = _userRepository
            .SingleResultQuery()
            .AndFilter(x => x.UserName == request.LecturerName);

        var lecturer = await _userRepository.SingleOrDefaultAsync(query);

        if (lecturer == null) throw new NotFoundException(nameof(ApplicationUser), request.LecturerName);

        entity.Lecturer = lecturer;
    }
}
