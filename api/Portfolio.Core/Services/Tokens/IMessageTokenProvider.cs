using Portfolio.Domain.Models.Blogs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Portfolio.Core.Services.Tokens;

public interface IMessageTokenProvider
{
    Task AddBlogTokensAsync(List<Token> tokens, BlogPost blogPost);

    Task AddBlogSubscriberTokensAsync(List<Token> tokens, BlogSubscriber subscriber);
}
