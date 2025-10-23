using Backend.Features.ShortUrls.Get;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Net.Http.Json;

namespace Url.Shortener.Tests.Features.ShortUrls.Get
{
    public sealed class GetShortUrlEndpointTests
    {
        [Theory]
        [InlineData("https://google.com", "google.com")]
        [InlineData("https://youtube.com", "youtube.com")]
        public async Task GetShortUrl_ReturnsOk(string originalUrl, string shortenedUrl)
        {
            var senderMock = new Mock<ISender>();

            senderMock.Setup(s =>
                s.Send(
                    It.Is<GetShortUrlQuery>(q => q.ShortUrl == shortenedUrl),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(originalUrl);

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

            var route = string.Format("/api/{0}", shortenedUrl);
            var response = await client.GetAsync(route);

            var expectedBody = new UrlResponse(originalUrl);
            var body = await response.Content.ReadFromJsonAsync<UrlResponse>();

            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(expectedBody, body);

            senderMock.Verify(s => s.Send(
                It.Is<GetShortUrlQuery>(c => c.ShortUrl == shortenedUrl),
                It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
