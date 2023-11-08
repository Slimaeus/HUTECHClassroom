using HUTECHClassroom.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HUTECHClassroom.Persistence.Configurations;

public sealed class ApplicationUserRoleConfiguration : IEntityTypeConfiguration<ApplicationUserRole>
{
    public void Configure(EntityTypeBuilder<ApplicationUserRole> builder)
    {
        builder.HasKey(x => new { x.UserId, x.RoleId });

        builder.HasOne(ur => ur.Role)
                .WithMany(r => r.ApplicationUserRoles)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

        builder.HasOne(ur => ur.User)
            .WithMany(r => r.ApplicationUserRoles)
            .HasForeignKey(ur => ur.UserId)
            .IsRequired();
    }
}
