using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portfolio.Domain.Models.Settings;

namespace Portfolio.Database.Configurations;

internal class BlogSettingsConfiguration : IEntityTypeConfiguration<BlogSettings>
{
    public void Configure(EntityTypeBuilder<BlogSettings> builder)
    {
        builder.HasData(new BlogSettings { 
            Id = 1,
            IsSendEmailOnCommentReply = true,
            EmailOnCommentReplySubjectTemplate = "%Comment.Name% replied to your comment",
            EmailOnCommentReplyTemplate = "%Comment.Name% replied with:' %Comment.MessageContent%', to the comment that you left on the blog post <a href=\"%BlogPost.Url\">%BlogPost.Title%</a>",

            IsSendEmailOnPublishing = true,
            EmailOnPublishingSubjectTemplate = "%BlogPost.Title% just got released!",
            EmailOnPublishingTemplate = "My new blog post titled: %BlogPost.Title% just got released visit this link to read it. %BlogPost.Url%",

            IsSendEmailOnSubscribing = true,
            EmailOnSubscribingSubjectTemplate = "Thanks for subscribing to my blog!",
            EmailOnSubscribingTemplate = "Thanks for subscribing to my blog, you will now receive an email every time i publish a new blog, if you ever wish to unsubscribe click this link: %BlogSubscriber.UnsubscribeURL%",
        });
    }
}
