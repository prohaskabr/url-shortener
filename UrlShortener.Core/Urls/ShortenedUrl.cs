namespace UrlShortener.Core.Urls;
public class ShortenedUrl
{
    public ShortenedUrl(Uri longUrl, string shortUrl, string createdBy, DateTimeOffset createdAt)
    {
        LongUrl = longUrl;
        ShortUrl = shortUrl;
        CreatedBy = createdBy;
        CreatedAt = createdAt;
    }

    public Uri LongUrl { get; }
    public string ShortUrl { get; }
    public string CreatedBy { get; }
    public DateTimeOffset CreatedAt { get; }
}
