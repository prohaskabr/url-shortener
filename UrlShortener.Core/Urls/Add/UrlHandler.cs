namespace UrlShortener.Core.Urls.Add;

public class UrlHandler
{
    private readonly ShortUrlGenerator _shortUrlGenerator;
    private readonly IUrlDataStore _urlDataStore;
    private readonly TimeProvider _timeProvider;

    public UrlHandler(ShortUrlGenerator shortUrlGenerator, IUrlDataStore urlDataStore, TimeProvider timeProvider)
    {
        _shortUrlGenerator = shortUrlGenerator;
        _urlDataStore = urlDataStore;
        _timeProvider = timeProvider;
    }

    public async Task<Result<AddUrlResponse>> HandleAsync(AddUrlRequest request, CancellationToken token)
    {
        if (string.IsNullOrEmpty(request.CreatedBy))
            return Errors.MissingCreatedBy;

        var shortUrl = new ShortenedUrl(request.LongUrl, _shortUrlGenerator.GenerateUniqueUrl(), request.CreatedBy, _timeProvider.GetUtcNow());

        await _urlDataStore.AddAsync(shortUrl, token);

        var result = new AddUrlResponse(shortUrl.LongUrl, shortUrl.ShortUrl);

        return result;
    }
}