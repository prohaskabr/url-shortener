namespace UrlShortener.Core.Urls.Add;

public record class AddUrlRequest(Uri LongUrl, string CreatedBy);
