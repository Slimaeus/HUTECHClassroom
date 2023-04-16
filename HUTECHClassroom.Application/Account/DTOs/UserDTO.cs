using HUTECHClassroom.Application.Common.DTOs;
using HUTECHClassroom.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace HUTECHClassroom.Application.Account.DTOs
{
    public record UserDTO : IEntityDTO
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }

        public static UserDTO Create(ApplicationUser user, string token = default)
            => new UserDTO { Id = user.Id, Email = user.Email, UserName = user.UserName, Token = token };
    }
}
