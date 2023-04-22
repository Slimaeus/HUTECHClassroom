using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Domain.Entities;

namespace HUTECHClassroom.Application.Projects.Commands.DeleteRangeProject;

public record DeleteRangeProjectCommand(IList<Guid> Ids) : DeleteRangeCommand(Ids);
public class DeleteRangeProjectCommandHandler : DeleteRangeCommandHandler<Project, DeleteRangeProjectCommand>
{
    public DeleteRangeProjectCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}
