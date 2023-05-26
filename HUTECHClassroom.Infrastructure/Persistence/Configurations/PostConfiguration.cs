using HUTECHClassroom.Domain.Constants;
using HUTECHClassroom.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HUTECHClassroom.Infrastructure.Persistence.Configurations;

public class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.Property(x => x.Content)
            .HasMaxLength(PostConstants.CONTENT_MAX_LENGTH)
            .IsRequired();

        builder.Property(x => x.Link)
            .HasMaxLength(CommonConstants.LINK_MAX_LENGTH);

        builder.HasMany(x => x.Comments)
            .WithOne(x => x.Post)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
