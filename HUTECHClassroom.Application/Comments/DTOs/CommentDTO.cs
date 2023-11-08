using HUTECHClassroom.Application.Common.DTOs;

namespace HUTECHClassroom.Application.Comments.DTOs;

public sealed record CommentDTO : BaseEntityDTO
{
    public string? Content { get; set; }
    public MemberDTO? User { get; set; }
    public CommentPostDTO? Post { get; set; }
}
