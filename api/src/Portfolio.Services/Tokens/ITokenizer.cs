namespace Portfolio.Services.Tokens;

public interface ITokenizer
{
    string Replace(string template, IEnumerable<Token> tokens, bool htmlEncode);
}
