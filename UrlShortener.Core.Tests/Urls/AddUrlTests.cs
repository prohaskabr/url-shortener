using FluentAssertions;
using Microsoft.Extensions.Time.Testing;
using UrlShortener.Core.Tests.TestDoubles;
using UrlShortener.Core.Urls.Add;


namespace UrlShortener.Core.Tests.Urls;

public class AddUrlTests
{
    private readonly InMemoryUrlDataStore _urlDataStore;
    private readonly AddUrlHandler _handler;
    private readonly FakeTimeProvider _timeProvider;

    public AddUrlTests()
    {
        _timeProvider = new FakeTimeProvider();
        var tokenProvider = new TokenProvider();
        tokenProvider.AssignRange(1, 5);

        var shortUrlGenerator = new ShortUrlGenerator(tokenProvider);

        _urlDataStore = new InMemoryUrlDataStore();

        _handler = new AddUrlHandler(shortUrlGenerator, _urlDataStore, _timeProvider);
    }

    [Fact]
    public async Task Should_return_shortened_url()
    {
        var request = CreateAddUrlRequest();

        var response = await _handler.HandleAsync(request, default);

        response.Succeeded.Should().BeTrue();
        response.Value!.ShortUrl.Should().NotBeNull();
        response.Value.ShortUrl.Should().Be(response.Value!.ShortUrl);
    }

    [Fact]
    public async Task Should_save_short_url()
    {
        var request = CreateAddUrlRequest();

        var response = await _handler.HandleAsync(request, default);
        
        response.Succeeded.Should().BeTrue();
        _urlDataStore.Should().ContainKey(response.Value!.ShortUrl);            
    }

    [Fact]
    public async Task Should_save_short_url_with_created_by_and_created_on()
    {
        var request = CreateAddUrlRequest();

        var response = await _handler.HandleAsync(request, default);

        response.Succeeded.Should().BeTrue();
        _urlDataStore.Should().ContainKey(response.Value!.ShortUrl);
        _urlDataStore[response.Value.ShortUrl].CreatedBy.Should().Be(request.CreatedBy);
        _urlDataStore[response.Value.ShortUrl].CreatedAt.Should().Be(_timeProvider.GetLocalNow());
    }

    [Fact]
    public async Task Should_return_error_if_created_by_is_empty()
    {
        var request =  CreateAddUrlRequest("");

        var response = await _handler.HandleAsync(request, default);

        response.Succeeded.Should().BeFalse();
        response.Error.Code.Should().Be("missing_value");
    }

    private static AddUrlRequest CreateAddUrlRequest(string createdBy = "admin")
    {
        return new AddUrlRequest(new Uri("https://www.example.com"), createdBy);
    }
}
