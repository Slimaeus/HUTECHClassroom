using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using HUTECHClassroom.Application.Comments.DTOs;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Domain.Entities;

namespace HUTECHClassroom.Application.Comments.Commands.DeleteComment;

public record DeleteCommentCommand(Guid Id) : DeleteCommand<CommentDTO>(Id);
public class DeleteCommentCommandHandler : DeleteCommandHandler<Comment, DeleteCommentCommand, CommentDTO>
{
    public DeleteCommentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}
