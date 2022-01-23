using Portfolio.Core.Events;
using Portfolio.Core.Services.QueuedEmails;
using Portfolio.Core.Services.Settings;
using Portfolio.Domain.Models;
using Portfolio.Domain.Models.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Core.Consumers;

public class MessageReceivedEvent : IConsumer<EntityInsertedEvent<Message, int>>
{
    #region Fields

    private readonly IQueuedEmailService _queuedEmailService;
    private readonly ISettingService<EmailSettings> _emailSettings;

    #endregion

    #region Constructor

    public MessageReceivedEvent(IQueuedEmailService queuedEmailService, ISettingService<EmailSettings> emailSettings)
    {
        _queuedEmailService = queuedEmailService;
        _emailSettings = emailSettings;
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

        var siteOwnerEmail = new QueuedEmail
        {
            Subject = $"You received a new message from ${eventMessage.Entity.FirstName}",
            Body = $"You received a new message from ${eventMessage.Entity.FirstName}, containing the message: ${eventMessage.Entity.MessageContent}",
            CreatedAtUTC = DateTime.UtcNow,
            To = emailSettings.SendTestEmailTo,
            ToName = emailSettings.DisplayName,
            From = emailSettings.Email,
            FromName = emailSettings.DisplayName,
        };

        var clientEmail = new QueuedEmail
        {
            Subject = $"Message received confirmation",
            Body = $"Hi, ${eventMessage.Entity.FirstName}, I wanted to let you know that I have received your message and will respond back as soon as possible.",
            CreatedAtUTC = DateTime.UtcNow,
            To = eventMessage.Entity.Email,
            ToName = eventMessage.Entity.FirstName,
            From = emailSettings.Email,
            FromName = emailSettings.DisplayName,
        };

        await _queuedEmailService.InsertAsync(siteOwnerEmail);
        await _queuedEmailService.InsertAsync(clientEmail);
    }

    #endregion

}
