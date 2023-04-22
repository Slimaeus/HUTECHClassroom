using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Domain.Entities;

namespace HUTECHClassroom.Application.Classrooms.Commands.DeleteRangeClassroom;

public record DeleteRangeClassroomCommand(IList<Guid> Ids) : DeleteRangeCommand(Ids);
public class DeleteRangeClassroomCommandHandler : DeleteRangeCommandHandler<Classroom, DeleteRangeClassroomCommand>
{
    public DeleteRangeClassroomCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}
