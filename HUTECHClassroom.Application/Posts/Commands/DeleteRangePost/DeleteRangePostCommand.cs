using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Domain.Entities;

namespace HUTECHClassroom.Application.Posts.Commands.DeleteRangePost;

public record DeleteRangePostCommand(IList<Guid> Ids) : DeleteRangeCommand(Ids);
public class DeleteRangePostCommandHandler : DeleteRangeCommandHandler<Post, DeleteRangePostCommand>
{
    public DeleteRangePostCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}
