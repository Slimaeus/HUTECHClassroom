using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Domain.Entities;

namespace HUTECHClassroom.Application.Groups.Commands.DeleteRangeGroup;

public record DeleteRangeGroupCommand(IList<Guid> Ids) : DeleteRangeCommand(Ids);
public class DeleteRangeGroupCommandHandler : DeleteRangeCommandHandler<Group, DeleteRangeGroupCommand>
{
    public DeleteRangeGroupCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}
