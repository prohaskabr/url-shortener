using FluentAssertions;

namespace UrlShortener.Core.Tests;

public class Base62EncodingTests
{
    [Theory]
    [InlineData(1, "1")]
    [InlineData(10, "A")]
    [InlineData(36, "a")]
    [InlineData(61, "z")]
    [InlineData(1000, "G8")]
    [InlineData(987654321, "14q60P")]
    public void Test(int number, string expected)
    {
        
        var result = Base62Encoding.Encode(number); 

        result.Should().Be(expected);
    }
}
