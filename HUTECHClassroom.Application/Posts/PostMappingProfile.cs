using HUTECHClassroom.Application.Posts.Commands.CreatePost;
using HUTECHClassroom.Application.Posts.Commands.UpdatePost;
using HUTECHClassroom.Application.Posts.DTOs;

namespace HUTECHClassroom.Application.Posts;

public class PostMappingProfile : Profile
{
    public PostMappingProfile()
    {
        CreateMap<Post, PostDTO>();
        CreateMap<CreatePostCommand, Post>();
        CreateMap<UpdatePostCommand, Post>()
            .ForAllMembers(options => options.Condition((src, des, srcValue, desValue) => srcValue != null));

        CreateMap<Classroom, PostClassroomDTO>();
        CreateMap<Comment, PostCommentDTO>();
    }
}
