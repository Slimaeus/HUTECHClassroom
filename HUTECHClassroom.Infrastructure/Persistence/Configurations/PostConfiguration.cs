using HUTECHClassroom.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HUTECHClassroom.Infrastructure.Persistence.Configurations;

public class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.Property(x => x.Content)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(x => x.Link)
            .HasMaxLength(200);

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.ClassroomId)
            .IsRequired();

        builder.HasMany(x => x.Comments)
            .WithOne(x => x.Post)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
