using Portfolio.Domain.Models.Blogs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Portfolio.Core.Services.Tokens;

public class MessageTokenProvider : IMessageTokenProvider
{

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
    }

    public Task AddBlogSubscriberTokensAsync(List<Token> tokens, BlogSubscriber subscriber)
    {
        if (tokens == null)
            tokens = new List<Token>();

        if (subscriber == null)
            return Task.CompletedTask;

        tokens.Add(new Token("BlogPost.EmailAddress", subscriber.EmailAddress));
        tokens.Add(new Token("BlogPost.Id", subscriber.Id));
        tokens.Add(new Token("BlogPost.Name", subscriber.Name));
        tokens.Add(new Token("BlogPost.CreatedAtUTC", subscriber.CreatedAtUTC.ToShortDateString()));
        tokens.Add(new Token("BlogPost.UpdatedAtUtc", subscriber.UpdatedAtUtc.ToShortDateString()));

        return Task.CompletedTask;
    }

    #endregion
    
}
