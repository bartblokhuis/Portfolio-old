using Portfolio.Core.Events;
using Portfolio.Domain.Models;
using Portfolio.Domain.Models.Blogs;
using Portfolio.Domain.Models.Settings;
using Portfolio.Services.Blogs;
using Portfolio.Services.BlogSubscribers;
using Portfolio.Services.QueuedEmails;
using Portfolio.Services.Settings;
using Portfolio.Services.Tokens;
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
    private readonly ISettingService<EmailSettings> _emailSettings;
    private readonly IMessageTokenProvider _messageTokenProvider;
    private readonly ITokenizer _tokenizer;
    private readonly IQueuedEmailService _queuedEmailService;

    #endregion

    #region Constructor

    public BlogPublishedEvent(IBlogPostService blogPostService, IBlogSubscriberService blogSubscriberService, ISettingService<BlogSettings> blogSettings, ISettingService<EmailSettings> emailSettings, IMessageTokenProvider messageTokenProvider, ITokenizer tokenizer, IQueuedEmailService queuedEmailService)
    {
        _blogPostService = blogPostService;
        _blogSubscriberService = blogSubscriberService;
        _blogSettings = blogSettings;
        _emailSettings = emailSettings;
        _messageTokenProvider = messageTokenProvider;
        _tokenizer = tokenizer;
        _queuedEmailService = queuedEmailService;
    }

    #endregion

    #region Utils

    public async Task PublishBlog(BlogPost blogPost)
    {
        var blogSettings = await _blogSettings.GetAsync();
        var emailSettings = await _emailSettings.GetAsync();
        if (blogSettings == null || !blogSettings.IsSendEmailOnPublishing || emailSettings == null)
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

            var queuedEmail = new QueuedEmail
            {
                Body = body,
                Subject = subject,
                FromName = emailSettings.DisplayName,
                From = emailSettings.Email,
                To = subscriber.EmailAddress,
                ToName = subscriber.EmailAddress,
            };

            await _queuedEmailService.InsertAsync(queuedEmail);
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

        var originalBlogPost = await _blogPostService.GetByIdAsync(blogPost.Entity.Id, true);
        if(originalBlogPost == null)
            throw new NullReferenceException(nameof(originalBlogPost));

        //Blog post is already published
        if (originalBlogPost.IsPublished)
            return;

        await PublishBlog(blogPost.Entity);
    }

    #endregion

}
