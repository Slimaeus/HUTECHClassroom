using HUTECHClassroom.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace HUTECHClassroom.Infrastructure.Persistence.Configurations
{
    public class GroupUserConfiguration : IEntityTypeConfiguration<GroupUser>
    {
        public void Configure(EntityTypeBuilder<GroupUser> builder)
        {
            builder.HasKey(x => new { x.UserId, x.GroupId });
        }
    }
}
