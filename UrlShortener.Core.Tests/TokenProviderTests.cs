using FluentAssertions;
using System.Collections.Concurrent;

namespace UrlShortener.Core.Tests;

public class TokenProviderTests
{
    [Fact]
    public void Should_get_token_from_start()
    {
        var tokenProvider = new TokenProvider();
        tokenProvider.AssignRange(10, 1000);

        tokenProvider.GetToken().Should().Be(10);
    }

    [Fact]
    public void Should_increment_token_on_get()
    {
        var tokenProvider = new TokenProvider();
        tokenProvider.AssignRange(10, 1000);
        tokenProvider.GetToken();

        tokenProvider.GetToken().Should().Be(11);
    }

    [Fact]
    public void Should_not_return_same_token_twice()
    {
        const int Start = 1;
        const int End = 1000;

        var tokenProvider = new TokenProvider();
        ConcurrentBag<long> tokens = new ConcurrentBag<long>();
        tokenProvider.AssignRange(Start, End);


        Parallel.ForEach(Enumerable.Range(Start, End), _ =>
        {
            tokens.Add(tokenProvider.GetToken());
        });

        tokens.Should().OnlyHaveUniqueItems();
    }
}
