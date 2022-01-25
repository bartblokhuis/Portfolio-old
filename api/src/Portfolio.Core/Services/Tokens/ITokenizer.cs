using System.Collections.Generic;

namespace Portfolio.Core.Services.Tokens;

public interface ITokenizer
{
    string Replace(string template, IEnumerable<Token> tokens, bool htmlEncode);
}
