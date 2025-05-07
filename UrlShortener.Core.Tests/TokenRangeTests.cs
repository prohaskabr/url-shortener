using FluentAssertions;

namespace UrlShortener.Core.Tests;
public class TokenRangeTests
{
    [Fact]
    public void When_start_token_is_greater_than_end_token_throws_exception()
    {
      var act = () => new TokenRange(10,5);

        act.Should().Throw<ArgumentException>()
            .WithMessage("End must be greater than or equal to start.");
    }
}
