using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Domain.Entities;

namespace HUTECHClassroom.Application.Comments.Commands.DeleteRangeComment;

public record DeleteRangeCommentCommand(IList<Guid> Ids) : DeleteRangeCommand(Ids);
public class DeleteRangeCommentCommandHandler : DeleteRangeCommandHandler<Comment, DeleteRangeCommentCommand>
{
    public DeleteRangeCommentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}
