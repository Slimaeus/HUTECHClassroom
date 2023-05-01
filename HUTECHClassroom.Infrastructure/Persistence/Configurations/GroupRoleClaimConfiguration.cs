using HUTECHClassroom.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HUTECHClassroom.Infrastructure.Persistence.Configurations;

public class GroupRoleClaimConfiguration : IEntityTypeConfiguration<GroupRoleClaim>
{
    public void Configure(EntityTypeBuilder<GroupRoleClaim> builder)
    {
        builder.HasOne(x => x.Role)
            .WithMany(x => x.GroupRoleClaims)
            .HasForeignKey(x => x.RoleId);
    }
}
