using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Domain.Entities;

namespace HUTECHClassroom.Application.Comments.Commands.UpdateComment;

public record UpdateCommentCommand(Guid Id) : UpdateCommand(Id)
{
    public string Content { get; set; }
}
public class UpdateCommentCommandHandler : UpdateCommandHandler<Comment, UpdateCommentCommand>
{
    public UpdateCommentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}
