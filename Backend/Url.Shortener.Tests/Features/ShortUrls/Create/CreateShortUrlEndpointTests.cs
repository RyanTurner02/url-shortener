using Url.Shortener.Features.ShortUrls.Create;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Net.Http.Json;

namespace Url.Shortener.Tests.Features.ShortUrls.Create
{
    public sealed class CreateShortUrlEndpointTests
    {
        [Fact]
        public async Task CreateShortUrl_ReturnsCreated()
        {
            var originalUrl = "https://example.com";

            var hash = "LliXArW";
            var shortenedUrl = $"http://localhost/{hash}";

            var senderMock = new Mock<ISender>();

            senderMock.Setup(s =>
                s.Send(
                    It.IsAny<CreateShortUrlCommand>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(hash);

            await using var app =
                new WebApplicationFactory<Program>()
                    .WithWebHostBuilder(builder =>
                    {
                        builder.ConfigureServices(services =>
                        {
                            var toRemove = services.Where(d => d.ServiceType == typeof(ISender)).ToList();
                            foreach (var d in toRemove) services.Remove(d);

                            services.AddSingleton(senderMock.Object);
                        });
                    });

            using var client = app.CreateClient();

            var route = "/";
            var payload = new { url = originalUrl };
            var response = await client.PostAsJsonAsync(route, payload);

            var expectedBody = new ShortUrlResponse(shortenedUrl);
            var body = await response.Content.ReadFromJsonAsync<ShortUrlResponse>();

            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(expectedBody, body);
            Assert.Equal(route, response.Headers.Location?.OriginalString);

            senderMock.Verify(
                s => s.Send(
                    It.Is<CreateShortUrlCommand>(
                        c => c.Url == originalUrl),
                    It.IsAny<CancellationToken>()),
                Times.Once);
        }
    }
}
