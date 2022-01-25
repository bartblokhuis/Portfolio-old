using Portfolio.Core.Events;
using Portfolio.Core.Services.Blogs;
using Portfolio.Core.Services.Comments;
using Portfolio.Core.Services.QueuedEmails;
using Portfolio.Core.Services.Settings;
using Portfolio.Core.Services.Tokens;
using Portfolio.Domain.Models;
using Portfolio.Domain.Models.Blogs;
using Portfolio.Domain.Models.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Core.Consumers;

public class OnCommentRepliedEvent : IConsumer<EntityInsertedEvent<Comment, int>>
{
    #region Fields

    private readonly ISettingService<BlogSettings> _blogSettings;
    private readonly ISettingService<EmailSettings> _emailSettings;
    private readonly IMessageTokenProvider _messageTokenProvider;
    private readonly ITokenizer _tokenizer;
    private readonly IQueuedEmailService _queuedEmailService;
    private readonly IBlogPostCommentService _blogPostCommentService;
    private readonly IBlogPostService _blogPostService;

    #endregion

    #region Constructor

    public OnCommentRepliedEvent(ISettingService<BlogSettings> blogSettings, ISettingService<EmailSettings> emailSettings, IMessageTokenProvider messageTokenProvider, ITokenizer tokenizer, IQueuedEmailService queuedEmailService, IBlogPostCommentService blogPostCommentService, IBlogPostService blogPostService)
    {
        _blogSettings = blogSettings;
        _emailSettings = emailSettings;
        _messageTokenProvider = messageTokenProvider;
        _tokenizer = tokenizer;
        _queuedEmailService = queuedEmailService;
        _blogPostCommentService = blogPostCommentService;
        _blogPostService = blogPostService;
    }

    #endregion

    #region Methods
    public async Task HandleEventAsync(EntityInsertedEvent<Comment, int> eventMessage)
    {
        var blogSettings = await _blogSettings.Get();
        var emailSettings = await _emailSettings.Get();

        if (blogSettings == null || !blogSettings.IsSendEmailOnCommentReply || emailSettings == null)
            return;

        if (eventMessage?.Entity?.ParentCommentId == null)
            return;

        var parentComment = await _blogPostCommentService.GetParentCommentAsync(eventMessage.Entity);
        if (parentComment == null || string.IsNullOrEmpty(parentComment.Email))
            return;

        var tokens = new List<Token>();
        await _messageTokenProvider.AddBlogPostCommentTokensAsync(tokens, eventMessage.Entity);

        if (parentComment.BlogPostId.HasValue)
        {
            var blogPost = await _blogPostService.GetByIdAsync((int)parentComment.BlogPostId, true);

            if (blogPost != null)
                await _messageTokenProvider.AddBlogTokensAsync(tokens, blogPost);
        }

        var subject = _tokenizer.Replace(blogSettings.EmailOnCommentReplySubjectTemplate, tokens, true);
        var body = _tokenizer.Replace(blogSettings.EmailOnCommentReplyTemplate, tokens, true);

        var queuedEmail = new QueuedEmail
        {
            Body = body,
            Subject = subject,
            FromName = emailSettings.DisplayName,
            From = emailSettings.Email,
            To = parentComment.Email,
            ToName = parentComment.Email,
        };

        await _queuedEmailService.InsertAsync(queuedEmail);

    }
    #endregion

}
