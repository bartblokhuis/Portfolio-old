using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Domain.Models.Settings;

namespace Portfolio.Database.Configurations;

internal class EmailSettingsConfiguration : IEntityTypeConfiguration<EmailSettings>
{
    public void Configure(EntityTypeBuilder<EmailSettings> builder)
    {
        builder.Property(t => t.DisplayName)
            .IsRequired()
            .HasMaxLength(64);

        builder.Property(t => t.Email)
            .IsRequired()
            .HasMaxLength(128);

        builder.Property(t => t.SendTestEmailTo)
            .IsRequired()
            .HasMaxLength(128);

        builder.Property(t => t.SiteOwnerEmailAddress)
            .IsRequired()
            .HasMaxLength(128);

        builder.Property(t => t.Host)
            .IsRequired()
            .HasMaxLength(128);
    }
}

