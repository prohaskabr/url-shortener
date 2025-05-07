namespace UrlShortener.Core;

public class TokenProvider
{
    TokenRange _tokenRange;
    public void AssignRange(TokenRange tokenRange)
    {
        _tokenRange = tokenRange;
    }

    public void AssignRange(long start, long end)
    {
        _tokenRange = new TokenRange(start, end);
    }

    public long GetToken()
    {
        return _tokenRange.Start;
    }
}
