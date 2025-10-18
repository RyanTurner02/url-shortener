using Backend.Features.ShortUrls.Create;
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
        public async Task CreateShortUrl_OriginalUrl_ResponseIsSuccessCode()
        {
            var senderMock = new Mock<ISender>();

            senderMock.Setup(s =>
                s.Send(It.IsAny<CreateShortUrlCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync("youtube.com");

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

            var route = "/api/create";
            var payload = new { url = "https://www.youtube.com" };
            var response = await client.PostAsJsonAsync(route, payload);

            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(route, response.Headers.Location?.OriginalString);

            senderMock.Verify(s => s.Send(
                It.Is<CreateShortUrlCommand>(c => c.Url == "https://www.youtube.com"),
                It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
