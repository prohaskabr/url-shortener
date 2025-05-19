using FluentAssertions;
using Microsoft.Extensions.Time.Testing;
using UrlShortener.Core.Tests.TestDoubles;
using UrlShortener.Core.Urls.Add;


namespace UrlShortener.Core.Tests.Urls;

public class AddUrlTests
{
    private readonly InMemoryUrlDataStore _urlDataStore;
    private readonly UrlHandler _handler;
    private readonly FakeTimeProvider _timeProvider;

    public AddUrlTests()
    {
        _timeProvider = new FakeTimeProvider();
        var tokenProvider = new TokenProvider();
        tokenProvider.AssignRange(1, 5);

        var shortUrlGenerator = new ShortUrlGenerator(tokenProvider);

        _urlDataStore = new InMemoryUrlDataStore();

        _handler = new UrlHandler(shortUrlGenerator, _urlDataStore, _timeProvider);
    }

    [Fact]
    public async Task Should_return_shortened_url()
    {
        var request = CreateAddUrlRequest();

        var response = await _handler.HandleAsync(request, default);

        response.ShortUrl.Should().NotBeNull();
        response.ShortUrl.Should().Be(response.ShortUrl);
    }

    [Fact]
    public async Task Should_save_short_url()
    {
        var request = CreateAddUrlRequest();

        var response = await _handler.HandleAsync(request, default);

        _urlDataStore.Should().ContainKey(response.ShortUrl);            
    }

    [Fact]
    public async Task Should_save_short_url_with_created_by_and_created_on()
    {
        var request = CreateAddUrlRequest();

        var response = await _handler.HandleAsync(request, default);

        _urlDataStore.Should().ContainKey(response.ShortUrl);
        _urlDataStore[response.ShortUrl].CreatedBy.Should().Be(request.CreatedBy);
        _urlDataStore[response.ShortUrl].CreatedAt.Should().Be(_timeProvider.GetLocalNow());
    }

    private static AddUrlRequest CreateAddUrlRequest()
    {
        return new AddUrlRequest(new Uri("https://www.example.com"),"admin");
    }
}
