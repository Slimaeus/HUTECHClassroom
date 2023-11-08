using HUTECHClassroom.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HUTECHClassroom.Persistence.Configurations;

public sealed class GroupUserConfiguration : IEntityTypeConfiguration<GroupUser>
{
    public void Configure(EntityTypeBuilder<GroupUser> builder)
    {
        builder.ToTable($"{nameof(GroupUser)}s");
        builder.HasKey(x => new { x.UserId, x.GroupId });
    }
}
