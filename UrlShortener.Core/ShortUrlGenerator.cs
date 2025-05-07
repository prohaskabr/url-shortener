namespace UrlShortener.Core;

public class ShortUrlGenerator
{
    private readonly TokenProvider _tokenProvider;

    public ShortUrlGenerator(TokenProvider tokenProvider)
    {
        _tokenProvider = tokenProvider;
    }
    public string GenerateUniqueUrl()
    {
        return Base62Encoding.Encode(_tokenProvider.GetToken());
    }
}
