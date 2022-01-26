using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Domain.Models;


namespace Portfolio.Database.Configurations;

internal class AboutMeConfiguration : IEntityTypeConfiguration<AboutMe>
{
    public void Configure(EntityTypeBuilder<AboutMe> builder)
    {
        builder.Property(nameof(AboutMe.Content)).HasMaxLength(256);
        builder.Property(nameof(AboutMe.Title)).HasMaxLength(128).IsRequired();

        builder.HasData(new AboutMe { Id = 1, Title = "Hi, welcome on my portfolio", Content = "" });
    }
}
