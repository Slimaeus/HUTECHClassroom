using HUTECHClassroom.Domain.Constants;
using HUTECHClassroom.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HUTECHClassroom.Persistence.Configurations;

public sealed class AnswerConfiguration : IEntityTypeConfiguration<Answer>
{
    public void Configure(EntityTypeBuilder<Answer> builder)
    {
        builder.Property(x => x.Description)
            .HasMaxLength(AnswerConstants.DESCRIPTION_MAX_LENGTH)
            .IsRequired();

        builder.Property(x => x.Link)
            .HasMaxLength(CommonConstants.LINK_MAX_LENGTH);

        builder.Property(x => x.Score)
            .HasDefaultValue(AnswerConstants.SCORE_DEFAULT_VALUE)
            .IsRequired();
    }
}
