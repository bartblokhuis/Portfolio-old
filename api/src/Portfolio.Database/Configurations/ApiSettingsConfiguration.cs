using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Domain.Models.Settings;

namespace Portfolio.Database.Configurations;

internal class ApiSettingsConfiguration : IEntityTypeConfiguration<ApiSettings>
{
    public void Configure(EntityTypeBuilder<ApiSettings> builder)
    {
        builder.Property(t => t.ApiUrl)
            .IsRequired()
            .HasMaxLength(64);

        builder.HasData(new ApiSettings { Id = 1, ApiUrl = "http://localhost:44301" });
    }
}
