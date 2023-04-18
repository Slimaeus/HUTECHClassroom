using HUTECHClassroom.Application.Common.DTOs;

namespace HUTECHClassroom.Application.Groups.DTOs
{
    public record GroupDTO : BaseEntityDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public MemberDTO Leader { get; set; }
        public GroupClassroomDTO Classroom { get; set; }
    }
}
