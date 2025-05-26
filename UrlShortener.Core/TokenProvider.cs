namespace UrlShortener.Core;

public class TokenProvider
{
    private readonly object _lock = new();
    private long _token = 0;
    TokenRange _tokenRange;

    public void AssignRange(TokenRange tokenRange)
    {
        _tokenRange = tokenRange;
        _token = _tokenRange.Start;
    }

    public void AssignRange(long start, long end)
    {
        AssignRange(new TokenRange(start, end));
    }

    public long GetToken()
    {
        lock (_lock)
        {
            return _token++;
        }
    }
}
