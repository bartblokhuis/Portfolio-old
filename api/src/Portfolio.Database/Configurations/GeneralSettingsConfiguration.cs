using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Domain.Models.Settings;

namespace Portfolio.Database.Configurations;

internal class GeneralSettingsConfiguration : IEntityTypeConfiguration<GeneralSettings>
{
    public void Configure(EntityTypeBuilder<GeneralSettings> builder)
    {
        builder.HasData(new GeneralSettings
        {
            Id = 1,
            CallToActionText = "About Me",
            LandingDescription = "Welcome on my portfolio website",
            LandingTitle = "Welcome!",
            ShowContactMeForm = true,
            ShowCopyRightInFooter = true,
            FooterTextBetweenCopyRightAndYear = true,
            FooterText = "My name"
        });
    }
}
