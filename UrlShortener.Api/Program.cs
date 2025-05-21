using Azure.Identity;
using UrlShortener.Api.Extensions;
using UrlShortener.Core.Urls.Add;

var builder = WebApplication.CreateBuilder(args);

var keyVaultName = builder.Configuration["KeyVaultName"];

if (!string.IsNullOrEmpty(keyVaultName))
{
    builder.Configuration.AddAzureKeyVault(
        new Uri($"https://{keyVaultName}.vault.azure.net/"),
        new DefaultAzureCredential());
}

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton(TimeProvider.System);
builder.Services.AddUrlFeature();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



app.MapPost("/api/urls", async (AddUrlHandler handler, AddUrlRequest request, CancellationToken token) =>
{
    var requestWithUser = request with { CreatedBy = "testuser@test.com" };


    var result = await handler.HandleAsync(requestWithUser, token);

    return result.Match(
        result => Results.Created($"api/url/{result.ShortUrl}", result),
        error => Results.BadRequest(error));
});


app.Run();
