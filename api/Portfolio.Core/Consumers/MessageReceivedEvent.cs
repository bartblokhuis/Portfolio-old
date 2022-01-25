using Portfolio.Core.Events;
using Portfolio.Core.Services.QueuedEmails;
using Portfolio.Core.Services.Settings;
using Portfolio.Core.Services.Tokens;
using Portfolio.Domain.Models;
using Portfolio.Domain.Models.Settings;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Portfolio.Core.Consumers;

public class MessageReceivedEvent : IConsumer<EntityInsertedEvent<Message, int>>
{
    #region Fields

    private readonly IQueuedEmailService _queuedEmailService;
    private readonly ISettingService<EmailSettings> _emailSettings;
    private readonly ISettingService<MessageSettings> _messageSettings;
    private readonly IMessageTokenProvider _messageTokenProvider;
    private readonly ITokenizer _tokenizer;

    #endregion

    #region Constructor

    public MessageReceivedEvent(IQueuedEmailService queuedEmailService, ISettingService<EmailSettings> emailSettings, ISettingService<MessageSettings> messageSettings, IMessageTokenProvider messageTokenProvider, ITokenizer tokenizer)
    {
        _queuedEmailService = queuedEmailService;
        _emailSettings = emailSettings;
        _messageSettings = messageSettings;
        _messageTokenProvider = messageTokenProvider;
        _tokenizer = tokenizer;
    }

    #endregion

    #region Methods

    public async Task HandleEventAsync(EntityInsertedEvent<Message, int> eventMessage)
    {
        if (eventMessage == null || eventMessage.Entity == null)
            return;

        var emailSettings = await _emailSettings.Get();
        if (emailSettings == null)
            return;

        var messageSettings = await _messageSettings.Get();
        if (messageSettings == null || (!messageSettings.IsSendSiteOwnerEmail && !messageSettings.IsSendConfirmationEmail))
            return;

        if (messageSettings.IsSendSiteOwnerEmail)
        {
            var tokens = new List<Token>();
            await _messageTokenProvider.AddMessageTokensAsync(tokens, eventMessage.Entity);

            var subject = _tokenizer.Replace(messageSettings.SiteOwnerSubjectTemplate, tokens, true);
            var body = _tokenizer.Replace(messageSettings.SiteOwnerTemplate, tokens, true);

            var queuedEmail = new QueuedEmail
            {
                Body = body,
                Subject = subject,
                FromName = emailSettings.DisplayName,
                From = emailSettings.Email,
                To = emailSettings.SiteOwnerEmailAddress,
                ToName = emailSettings.SiteOwnerEmailAddress,
            };

            await _queuedEmailService.InsertAsync(queuedEmail);
        }
        if(messageSettings.IsSendConfirmationEmail)
        {
            var tokens = new List<Token>();
            await _messageTokenProvider.AddMessageTokensAsync(tokens, eventMessage.Entity);

            var subject = _tokenizer.Replace(messageSettings.ConfirmationEmailSubjectTemplate, tokens, true);
            var body = _tokenizer.Replace(messageSettings.ConfirmationEmailTemplate, tokens, true);

            var queuedEmail = new QueuedEmail
            {
                Body = body,
                Subject = subject,
                FromName = emailSettings.DisplayName,
                From = emailSettings.Email,
                To = eventMessage.Entity.Email,
                ToName = eventMessage.Entity.Email,
            };

            await _queuedEmailService.InsertAsync(queuedEmail);
        }
    }

    #endregion

}
