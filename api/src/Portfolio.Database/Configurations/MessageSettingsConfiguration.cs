using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Domain.Models.Settings;

namespace Portfolio.Database.Configurations;

internal class MessageSettingsConfiguration : IEntityTypeConfiguration<MessageSettings>
{
    public void Configure(EntityTypeBuilder<MessageSettings> builder)
    {
        builder.HasData(new MessageSettings {
            Id = 1,
            IsSendConfirmationEmail = true,
            ConfirmationEmailSubjectTemplate = "Thanks for sending me a message!",
            ConfirmationEmailTemplate ="Thanks for reaching out %Message.Name%, I received your message and will respond as soon as possible",
            
            IsSendSiteOwnerEmail = true,
            SiteOwnerSubjectTemplate = "Someone send you a message",
            SiteOwnerTemplate = "%Message.Name%, has sent you the following message: %Message.MessageContent%"
        });;
    }
}

