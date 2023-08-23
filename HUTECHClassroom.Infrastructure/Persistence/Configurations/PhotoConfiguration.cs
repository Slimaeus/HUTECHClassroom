using HUTECHClassroom.Domain.Constants;
using HUTECHClassroom.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HUTECHClassroom.Infrastructure.Persistence.Configurations;

public class PhotoConfiguration : IEntityTypeConfiguration<Photo>
{
    public void Configure(EntityTypeBuilder<Photo> builder)
    {
        builder.Property<string>(x => x.Title)
            .HasMaxLength(PhotoConstants.TITLE_MAX_LENGTH);

        builder.HasOne(a => a.User)
            .WithOne(u => u.Avatar)
            .HasForeignKey<Photo>(a => a.UserId);
    }
}
