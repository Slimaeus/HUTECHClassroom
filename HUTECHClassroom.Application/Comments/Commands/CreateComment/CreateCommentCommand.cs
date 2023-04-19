using AutoMapper;
using EntityFrameworkCore.Repository.Interfaces;
using EntityFrameworkCore.UnitOfWork.Interfaces;
using HUTECHClassroom.Application.Comments.DTOs;
using HUTECHClassroom.Application.Common.Exceptions;
using HUTECHClassroom.Application.Common.Requests;
using HUTECHClassroom.Domain.Entities;

namespace HUTECHClassroom.Application.Comments.Commands.CreateComment;

public record CreateCommentCommand : CreateCommand<CommentDTO>
{
    public string Content { get; set; }
    public string UserName { get; set; }
    public Guid PostId { get; set; }
}
public class CreateCommentCommandHandler : CreateCommandHandler<Comment, CreateCommentCommand, CommentDTO>
{
    private readonly IRepository<ApplicationUser> _userRepository;
    private readonly IRepository<Post> _postRepository;
    public CreateCommentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
        _userRepository = unitOfWork.Repository<ApplicationUser>();
        _postRepository = unitOfWork.Repository<Post>();
    }
    protected override async Task ValidateAdditionalField(CreateCommentCommand request, Comment entity)
    {
        var userQuery = _userRepository
            .SingleResultQuery()
            .AndFilter(x => x.UserName == request.UserName);

        var user = await _userRepository.SingleOrDefaultAsync(userQuery);

        if (user == null) throw new NotFoundException(nameof(ApplicationUser), request.UserName);

        entity.User = user;

        var postQuery = _postRepository
            .SingleResultQuery()
            .AndFilter(x => x.Id == request.PostId);

        var post = await _postRepository.SingleOrDefaultAsync(postQuery);

        if (post == null) throw new NotFoundException(nameof(Post), request.PostId);

        entity.Post = post;
    }
}
