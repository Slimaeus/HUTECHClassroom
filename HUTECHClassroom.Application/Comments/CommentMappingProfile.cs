using AutoMapper;
using HUTECHClassroom.Application.Comments.Commands.CreateComment;
using HUTECHClassroom.Application.Comments.Commands.UpdateComment;
using HUTECHClassroom.Application.Comments.DTOs;
using HUTECHClassroom.Domain.Entities;

namespace HUTECHClassroom.Application.Comments;

public class CommentMappingProfile : Profile
{
    public CommentMappingProfile()
    {
        CreateMap<Comment, CommentDTO>();
        CreateMap<CreateCommentCommand, Comment>();
        CreateMap<UpdateCommentCommand, Comment>()
            .ForAllMembers(options => options.Condition((src, des, srcValue, desValue) => srcValue != null));

        CreateMap<Post, CommentPostDTO>();
    }
}
