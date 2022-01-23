using Portfolio.Core.Events;
using Portfolio.Core.Interfaces.Common;
using Portfolio.Core.Services.Blogs;
using Portfolio.Core.Services.BlogSubscribers;
using Portfolio.Core.Services.Settings;
using Portfolio.Core.Services.Tokens;
using Portfolio.Domain.Models.Blogs;
using Portfolio.Domain.Models.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Core.Consumers;

public class BlogPublishedEvent : IConsumer<EntityInsertedEvent<BlogPost, int>>, IConsumer<EntityUpdatedEvent<BlogPost, int>>
{
    #region Fields

    private readonly IBlogPostService _blogPostService;
    private readonly IBlogSubscriberService _blogSubscriberService;
    private readonly ISettingService<BlogSettings> _blogSettings;
    private readonly IMessageTokenProvider _messageTokenProvider;
    private readonly ITokenizer _tokenizer;
    private readonly IEmailService _emailService;

    #endregion

    #region Constructor

    public BlogPublishedEvent(IBlogPostService blogPostService, IBlogSubscriberService blogSubscriberService, ISettingService<BlogSettings> blogSettings, IMessageTokenProvider messageTokenProvider, ITokenizer tokenizer, IEmailService emailService)
    {
        _blogPostService = blogPostService;
        _blogSubscriberService = blogSubscriberService;
        _blogSettings = blogSettings;
        _messageTokenProvider = messageTokenProvider;
        _tokenizer = tokenizer;
        _emailService = emailService;
    }

    #endregion

    #region Utils

    public async Task PublishBlog(BlogPost blogPost)
    {
        var blogSettings = await _blogSettings.Get();
        if (blogSettings == null || !blogSettings.IsSendEmailOnPublishing)
            return;

        var subscribers = await _blogSubscriberService.GetAllAsync();
        if (subscribers == null || !subscribers.Any())
            return;

        foreach (var subscriber in subscribers)
        {
            var tokens = new List<Token>();

            await _messageTokenProvider.AddBlogTokensAsync(tokens, blogPost);
            await _messageTokenProvider.AddBlogSubscriberTokensAsync(tokens, subscriber);

            var subject = _tokenizer.Replace(blogSettings.EmailOnPublishingSubjectTemplate, tokens, true);
            var body = _tokenizer.Replace(blogSettings.EmailOnPublishingTemplate, tokens, true);

            await _emailService.SendEmail(subscriber.EmailAddress, subscriber.EmailAddress, subject, body);
        }
    }

    #endregion

    #region Methods

    public async Task HandleEventAsync(EntityInsertedEvent<BlogPost, int> blogPost)
    {
        if (blogPost == null || blogPost.Entity == null || !blogPost.Entity.IsPublished)
            return;

        await PublishBlog(blogPost.Entity);
    }

    public async Task HandleEventAsync(EntityUpdatedEvent<BlogPost, int> blogPost)
    {
        if (blogPost == null || blogPost.Entity == null || !blogPost.Entity.IsPublished)
            return;

        var originalBlogPost = await _blogPostService.GetById(blogPost.Entity.Id, true);
        if(originalBlogPost == null)
            throw new NullReferenceException(nameof(originalBlogPost));

        //Blog post is already published
        if (originalBlogPost.IsPublished)
            return;

        await PublishBlog(blogPost.Entity);
    }

    #endregion

}
