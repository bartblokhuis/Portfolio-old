using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Domain.Models.Localization;

namespace Portfolio.Database.Configurations;

internal class LanguagesConfiguration : IEntityTypeConfiguration<Language>
{
    public void Configure(EntityTypeBuilder<Language> builder)
    {
        builder.Property(nameof(Language.Name)).HasMaxLength(128).IsRequired();

        builder.HasData(new Language { Id = 1, DisplayNumber = 0, IsPublished = true, LanguageCulture = "en-US", Name = "English", FlagImageFilePath = "languages/united-33135.svg" });
        builder.HasData(new Language { Id = 2, DisplayNumber = 1, IsPublished = false, LanguageCulture = "nl-NL", Name = "Dutch", FlagImageFilePath = "languages/netherlands-33035.svg" });
    }
}