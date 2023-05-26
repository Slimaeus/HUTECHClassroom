using HUTECHClassroom.Domain.Constants;
using HUTECHClassroom.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HUTECHClassroom.Infrastructure.Persistence.Configurations;

public class GroupConfiguration : IEntityTypeConfiguration<Group>
{
    public void Configure(EntityTypeBuilder<Group> builder)
    {
        builder.Property(x => x.Name)
            .HasMaxLength(GroupConstants.NAME_MAX_LENGTH)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(GroupConstants.DESCRIPTION_MAX_LENGTH);

        builder.HasMany(x => x.Projects)
            .WithOne(x => x.Group)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
