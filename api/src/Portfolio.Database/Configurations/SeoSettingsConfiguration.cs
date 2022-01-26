using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Domain.Models.Settings;

namespace Portfolio.Database.Configurations;

internal class SeoSettingsConfiguration : IEntityTypeConfiguration<SeoSettings>
{
    public void Configure(EntityTypeBuilder<SeoSettings> builder)
    {
        builder.Property(t => t.Title)
            .IsRequired()
            .HasMaxLength(64);

        builder.HasData(
            new SeoSettings
            {
                Id = 1,
                Title = "My portfolio"
            });
    }
}


