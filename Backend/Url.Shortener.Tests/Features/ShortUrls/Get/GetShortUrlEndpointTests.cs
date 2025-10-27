using Url.Shortener.Features.ShortUrls.Get;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Net.Http.Json;

namespace Url.Shortener.Tests.Features.ShortUrls.Get
{
    public sealed class GetShortUrlEndpointTests
    {
        [Fact]
        public async Task GetShortUrl_ReturnsOk()
        {
            var originalUrl = "https://example.com/";
            var shortenedUrlHash = "LliXArW";

            var senderMock = new Mock<ISender>();

            senderMock.Setup(s =>
                s.Send(
                    It.Is<GetShortUrlQuery>(q => q.ShortUrl == shortenedUrlHash),
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

            var route = string.Format("/{0}", shortenedUrlHash);
            var response = await client.GetAsync(route);

            var expectedBody = new UrlResponse(originalUrl);
            var body = await response.Content.ReadFromJsonAsync<UrlResponse>();

            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(expectedBody, body);

            senderMock.Verify(s => s.Send(
                It.Is<GetShortUrlQuery>(c => c.ShortUrl == shortenedUrlHash),
                It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
