﻿using Portfolio.Domain.Models;
using Portfolio.Domain.Models.Blogs;

namespace Portfolio.Services.Tokens;

public interface IMessageTokenProvider
{
    Task AddBlogTokensAsync(List<Token> tokens, BlogPost blogPost);

    Task AddBlogSubscriberTokensAsync(List<Token> tokens, BlogSubscriber subscriber);

    Task AddBlogPostCommentTokensAsync(List<Token> tokens, Comment comment);

    Task AddMessageTokensAsync(List<Token> tokens, Message message);
}
