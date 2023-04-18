using HUTECHClassroom.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HUTECHClassroom.Infrastructure.Persistence.Configurations;

public class MissionUserConfiguration : IEntityTypeConfiguration<MissionUser>
{
    public void Configure(EntityTypeBuilder<MissionUser> builder)
    {
        builder.HasKey(x => new { x.UserId, x.MissionId });
    }
}
