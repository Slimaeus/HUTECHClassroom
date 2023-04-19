using HUTECHClassroom.Application.Common.DTOs;

namespace HUTECHClassroom.Application.Classrooms.DTOs;

public record ClassroomPostDTO : BaseEntityDTO
{
    public string Content { get; set; }
    public string Link { get; set; }
    public MemberDTO User { get; set; }
}
