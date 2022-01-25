using Portfolio.Services.Settings;
using Portfolio.Domain.Models;
using Portfolio.Domain.Models.Blogs;
using Portfolio.Domain.Models.Settings;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Portfolio.Services.Tokens;

public class MessageTokenProvider : IMessageTokenProvider
{
    #region Fields

    private readonly ISettingService<PublicSiteSettings> _publicSiteSettings;

    #endregion

    #region Constructor

    public MessageTokenProvider(ISettingService<PublicSiteSettings> publicSiteSettings)
    {
        _publicSiteSettings = publicSiteSettings;
    }

    #endregion

    #region Utils

    protected async Task<string> GetPublicSiteUrl()
    {
        var publicSiteSettings = await _publicSiteSettings.GetAsync();
        if(publicSiteSettings == null)
            throw new NullReferenceException(nameof(publicSiteSettings));

        if (string.IsNullOrEmpty(publicSiteSettings.PublicSiteUrl))
            return "/";

        //Ensure that the site url ends with a /
        return publicSiteSettings.PublicSiteUrl.EndsWith("/") ? publicSiteSettings.PublicSiteUrl : publicSiteSettings.PublicSiteUrl + "/";
    }

    #endregion

    #region Methods

    public async Task AddBlogTokensAsync(List<Token> tokens, BlogPost blogPost)
    {
        if (tokens == null)
            tokens = new List<Token>();

        if (blogPost == null)
            return;

        tokens.Add(new Token("BlogPost.Id", blogPost.Id));
        tokens.Add(new Token("BlogPost.Title", blogPost.Title));
        tokens.Add(new Token("BlogPost.Description", blogPost.Description));
        tokens.Add(new Token("BlogPost.Content", blogPost.Content));
        tokens.Add(new Token("BlogPost.IsPublished", blogPost.IsPublished));
        tokens.Add(new Token("BlogPost.DisplayNumber", blogPost.DisplayNumber));
        tokens.Add(new Token("BlogPost.MetaTitle", blogPost.MetaTitle));
        tokens.Add(new Token("BlogPost.MetaDescription", blogPost.MetaDescription));
        tokens.Add(new Token("BlogPost.CreatedAtUTC", blogPost.CreatedAtUTC.ToShortDateString()));
        tokens.Add(new Token("BlogPost.UpdatedAtUtc", blogPost.UpdatedAtUtc.ToShortDateString()));
        tokens.Add(new Token("BlogPost.Url", (await GetPublicSiteUrl()) + "blog/" + blogPost.Title));
    }

    public async Task AddBlogSubscriberTokensAsync(List<Token> tokens, BlogSubscriber subscriber)
    {
        if (tokens == null)
            tokens = new List<Token>();

        if (subscriber == null)
            return;

        tokens.Add(new Token("BlogSubscriber.EmailAddress", subscriber.EmailAddress));
        tokens.Add(new Token("BlogSubscriber.Id", subscriber.Id));
        tokens.Add(new Token("BlogSubscriber.Name", subscriber.Name));
        tokens.Add(new Token("BlogSubscriber.CreatedAtUTC", subscriber.CreatedAtUTC.ToShortDateString()));
        tokens.Add(new Token("BlogSubscriber.UpdatedAtUtc", subscriber.UpdatedAtUtc.ToShortDateString()));
        tokens.Add(new Token("BlogSubscriber.UnsubscribeURL", (await GetPublicSiteUrl()) + "blog/unsubscribe/" + subscriber.Id.ToString()));

        return;
    }

    public async Task AddBlogPostCommentTokensAsync(List<Token> tokens, Comment comment)
    {
        if (tokens == null)
            tokens = new List<Token>();

        if (comment == null)
            return;

        tokens.Add(new Token("Comment.Name", comment.Name));
        tokens.Add(new Token("Comment.Content", comment.Content));
    }

    public async Task AddMessageTokensAsync(List<Token> tokens, Message message)
    {
        if (tokens == null)
            tokens = new List<Token>();

        if (message == null)
            return;

        tokens.Add(new Token("Message.Name", message.FirstName));
        tokens.Add(new Token("Message.MessageContent", message.MessageContent));
    }
    #endregion

}
