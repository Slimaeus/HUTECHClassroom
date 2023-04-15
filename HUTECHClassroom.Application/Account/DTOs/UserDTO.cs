using HUTECHClassroom.Application.Common.DTOs;

namespace HUTECHClassroom.Application.Account.DTOs
{
    public record UserDTO : IEntityDTO
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
