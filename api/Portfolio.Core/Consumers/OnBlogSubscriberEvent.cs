using Portfolio.Core.Events;
using Portfolio.Core.Interfaces.Common;
using Portfolio.Core.Services.Settings;
using Portfolio.Core.Services.Tokens;
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
    private readonly IMessageTokenProvider _messageTokenProvider;
    private readonly ITokenizer _tokenizer;
    private readonly IEmailService _emailService;

    #endregion

    #region Constructor

    public OnBlogSubscriberEvent(ISettingService<BlogSettings> blogSettings, IMessageTokenProvider messageTokenProvider, ITokenizer tokenizer, IEmailService emailService)
    {
        _blogSettings = blogSettings;
        _messageTokenProvider = messageTokenProvider;
        _tokenizer = tokenizer;
        _emailService = emailService;
    }

    #endregion

    #region Methods
    public async Task HandleEventAsync(EntityInsertedEvent<BlogSubscriber, Guid> eventMessage)
    {
        var blogSettings = await _blogSettings.Get();
        if (blogSettings == null || !blogSettings.IsSendEmailOnSubscribing)
            return;

        if (eventMessage?.Entity == null)
            return;

        var subscriber = eventMessage.Entity;

        var tokens = new List<Token>();

        await _messageTokenProvider.AddBlogSubscriberTokensAsync(tokens, subscriber);

        var subject = _tokenizer.Replace(blogSettings.EmailOnSubscribingSubjectTemplate, tokens, true);
        var body = _tokenizer.Replace(blogSettings.EmailOnSubscribingTemplate, tokens, true);

        await _emailService.SendEmail(subscriber.EmailAddress, subscriber.EmailAddress, subject, body);

    }
    #endregion

}
