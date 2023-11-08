using HUTECHClassroom.Application.Common.DTOs;

namespace HUTECHClassroom.Application.Posts.DTOs;

public record PostDTO : BaseEntityDTO
{
    public string? Content { get; set; }
    public string? Link { get; set; }
    public MemberDTO? User { get; set; }
    public PostClassroomDTO? Classroom { get; set; }
}
