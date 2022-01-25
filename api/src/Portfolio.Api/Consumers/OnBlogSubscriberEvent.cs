using Portfolio.Core.Events;
using Portfolio.Services.Common;
using Portfolio.Services.QueuedEmails;
using Portfolio.Services.Settings;
using Portfolio.Services.Tokens;
using Portfolio.Domain.Models;
using Portfolio.Domain.Models.Blogs;
using Portfolio.Domain.Models.Settings;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Portfolio.Core.Consumers;

public class OnBlogSubscriberEvent : IConsumer<EntityInsertedEvent<BlogSubscriber, Guid>>
{
    #region Fields

    private readonly ISettingService<BlogSettings> _blogSettings;
    private readonly ISettingService<EmailSettings> _emailSettings;
    private readonly IMessageTokenProvider _messageTokenProvider;
    private readonly ITokenizer _tokenizer;
    private readonly IQueuedEmailService _queuedEmailService;

    #endregion

    #region Constructor

    public OnBlogSubscriberEvent(ISettingService<BlogSettings> blogSettings, ISettingService<EmailSettings> emailSettings, IMessageTokenProvider messageTokenProvider, ITokenizer tokenizer, IQueuedEmailService queuedEmailService)
    {
        _blogSettings = blogSettings;
        _emailSettings = emailSettings;
        _messageTokenProvider = messageTokenProvider;
        _tokenizer = tokenizer;
        _queuedEmailService = queuedEmailService;
    }

    #endregion

    #region Methods
    public async Task HandleEventAsync(EntityInsertedEvent<BlogSubscriber, Guid> eventMessage)
    {
        var blogSettings = await _blogSettings.GetAsync();
        var emailSettings = await _emailSettings.GetAsync();

        if (blogSettings == null || !blogSettings.IsSendEmailOnSubscribing || emailSettings == null)
            return;

        if (eventMessage?.Entity == null)
            return;

        var subscriber = eventMessage.Entity;

        var tokens = new List<Token>();

        await _messageTokenProvider.AddBlogSubscriberTokensAsync(tokens, subscriber);

        var subject = _tokenizer.Replace(blogSettings.EmailOnSubscribingSubjectTemplate, tokens, true);
        var body = _tokenizer.Replace(blogSettings.EmailOnSubscribingTemplate, tokens, true);

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
    #endregion

}
