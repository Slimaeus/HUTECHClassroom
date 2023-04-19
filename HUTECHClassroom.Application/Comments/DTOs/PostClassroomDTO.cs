using HUTECHClassroom.Application.Common.DTOs;

namespace HUTECHClassroom.Application.Comments.DTOs;

public record CommentPostDTO : BaseEntityDTO
{
    public string Content { get; set; }
    public string Link { get; set; }
}
