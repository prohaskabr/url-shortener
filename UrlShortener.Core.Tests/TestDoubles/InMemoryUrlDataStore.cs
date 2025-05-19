using UrlShortener.Core.Urls;
using UrlShortener.Core.Urls.Add;

namespace UrlShortener.Core.Tests.TestDoubles;

public class InMemoryUrlDataStore : Dictionary<string, ShortenedUrl>, IUrlDataStore
{
    public Task AddAsync(ShortenedUrl shortened, CancellationToken token)
    {
        Add(shortened.ShortUrl, shortened);

        return Task.CompletedTask;
    }
}
