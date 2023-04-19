using HUTECHClassroom.Application.Common.DTOs;

namespace HUTECHClassroom.Application.Posts.DTOs;

public record PostCommentDTO : BaseEntityDTO
{
    public string Content { get; set; }
    public MemberDTO User { get; set; }
}
