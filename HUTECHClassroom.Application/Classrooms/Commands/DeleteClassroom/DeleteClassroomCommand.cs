using HUTECHClassroom.Application.Classrooms.DTOs;
using HUTECHClassroom.Application.Common.Requests;

namespace HUTECHClassroom.Application.Classrooms.Commands.DeleteClassroom;

public record DeleteClassroomCommand(Guid Id) : DeleteCommand<ClassroomDTO>(Id);
public class DeleteClassroomCommandHandler : DeleteCommandHandler<Classroom, DeleteClassroomCommand, ClassroomDTO>
{
    public DeleteClassroomCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}
