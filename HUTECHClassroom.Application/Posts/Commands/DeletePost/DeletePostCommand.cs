using AutoMapper;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Application.Posts.DTOs;
using HUTECHClassroom.Domain.Entities;

namespace HUTECHClassroom.Application.Posts.Commands.DeletePost;

public record DeletePostCommand(Guid Id) : DeleteCommand<PostDTO>(Id);
public class DeletePostCommandHandler : DeleteCommandHandler<Post, DeletePostCommand, PostDTO>
{
    public DeletePostCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }
}
