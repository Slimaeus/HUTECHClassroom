using HUTECHClassroom.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HUTECHClassroom.Infrastructure.Persistence.Configurations;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(x => x.FirstName)
            .IsRequired();

        builder.Property(x => x.LastName)
            .IsRequired();

        builder.HasMany(x => x.Classrooms)
            .WithOne(x => x.Lecturer)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(x => x.Groups)
            .WithOne(x => x.Leader)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(x => x.Posts)
            .WithOne(x => x.User)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(x => x.Answers)
            .WithOne(x => x.User)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(x => x.Comments)
            .WithOne(x => x.User)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
