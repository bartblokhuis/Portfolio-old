using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Domain.Models.Settings;

namespace Portfolio.Database.Configurations;

internal class PublicSiteSettingsConfiguration : IEntityTypeConfiguration<PublicSiteSettings>
{
    public void Configure(EntityTypeBuilder<PublicSiteSettings> builder)
    {
        builder.HasData(new PublicSiteSettings
        {
            Id = 1,
            PublicSiteUrl = "http://localhost:4200"
        });
    }
}

