using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using UrlShortener.Api;
using UrlShortener.Core.Urls;
using UrlShortener.Core.Urls.Add;

namespace UrlShortener.Tests;

public class ApiFixture : WebApplicationFactory<IApiAssemblyMarker>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(context =>
        {
            context.RemoveAll<IUrlDataStore>();

            context.AddSingleton<IUrlDataStore, InMemoryUrlDataStore>();
        });


        base.ConfigureWebHost(builder);
    }
}

public class InMemoryUrlDataStore : IUrlDataStore
{
    private readonly Dictionary<string, ShortenedUrl> _store = new();
    public Task AddAsync(ShortenedUrl url, CancellationToken token)
    {
        _store[url.ShortUrl] = url;
        return Task.CompletedTask;
    }
}
