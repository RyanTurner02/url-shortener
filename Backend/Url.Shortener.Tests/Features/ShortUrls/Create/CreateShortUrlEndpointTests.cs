using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Net.Http.Json;
using Url.Shortener.Features.ShortUrls.Create;

namespace Url.Shortener.Tests.Features.ShortUrls.Create
{
    /// <summary>
    /// Tests the <see cref="CreateShortUrlEndpoint"/> class.
    /// </summary>
    public sealed class CreateShortUrlEndpointTests
    {
        /// <summary>
        /// The endpoint route.
        /// </summary>
        private const string ROUTE = "/";

        /// <summary>
        /// Tests creating a short URL with an invalid URL.
        /// </summary>
        /// <param name="url">The invalid URL.</param>
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("example")]
        [InlineData("example.com")]
        [InlineData("www.example.com")]
        public async Task CreateShortUrl_InvalidUrl_ReturnsBadRequest(string url)
        {
            await using var app = new WebApplicationFactory<Program>();
            using var client = app.CreateClient();

            var payload = new { url = url };
            var response = await client.PostAsJsonAsync(ROUTE, payload);
            var body = await response.Content.ReadFromJsonAsync<InvalidUrlResponse>();

            Assert.Equal(StatusCodes.Status400BadRequest, (int)response.StatusCode);

            Assert.NotNull(body);
            Assert.Equal("InvalidUrl", body.Error);
            Assert.Equal("URL must be valid and not exceed 128 characters.", body.Message);
        }

        /// <summary>
        /// Tests that an empty generated short URL results in a conflict response.
        /// </summary>
        [Fact]
        public async Task CreateShortUrl_EmptyShortUrl_ReturnsConflict()
        {
            var hash = string.Empty;
            var originalUrl = "https://example.com/";

            var senderMock = new Mock<ISender>();
            senderMock.Setup(s =>
                s.Send(
                    It.IsAny<CreateShortUrlCommand>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(hash);

            await using var app = CreateApp(senderMock.Object);
            using var client = app.CreateClient();

            var payload = new { url = originalUrl };
            var response = await client.PostAsJsonAsync(ROUTE, payload);
            var body = await response.Content.ReadFromJsonAsync<DuplicateConflictResponse>();

            Assert.Equal(StatusCodes.Status409Conflict, (int)response.StatusCode);

            Assert.NotNull(body);
            Assert.Equal("DuplicateConflict", body.Error);
            Assert.Equal("Failed to generate a unique short URL. Please try again later.", body.Message);

            senderMock.Verify(
                s => s.Send(
                    It.Is<CreateShortUrlCommand>(
                        c => c.Url == originalUrl),
                    It.IsAny<CancellationToken>()),
                Times.Once);
        }

        /// <summary>
        /// Tests that creating a valid URL results in a created response with the shortened URL.
        /// </summary>
        [Fact]
        public async Task CreateShortUrl_ValidUrl_ReturnsCreated()
        {
            var hash = "ShortUrl";
            var shortenedUrl = $"http://localhost/{hash}";
            var originalUrl = "https://example.com/";

            var senderMock = new Mock<ISender>();
            senderMock.Setup(s =>
                s.Send(
                    It.IsAny<CreateShortUrlCommand>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(hash);

            await using var app = CreateApp(senderMock.Object);
            using var client = app.CreateClient();

            var payload = new { url = originalUrl };
            var response = await client.PostAsJsonAsync(ROUTE, payload);

            var expectedBody = new ShortUrlResponse(shortenedUrl);
            var body = await response.Content.ReadFromJsonAsync<ShortUrlResponse>();

            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(expectedBody, body);

            senderMock.Verify(
                s => s.Send(
                    It.Is<CreateShortUrlCommand>(
                        c => c.Url == originalUrl),
                    It.IsAny<CancellationToken>()),
                Times.Once);
        }

        /// <summary>
        /// Creates a web application used for testing.
        /// </summary>
        /// <param name="senderMock">The ISender mock object.</param>
        /// <returns>The web application used for testing.</returns>
        private WebApplicationFactory<Program> CreateApp(ISender senderMock)
        {
            return new WebApplicationFactory<Program>()
                    .WithWebHostBuilder(builder =>
                    {
                        builder.ConfigureServices(services =>
                        {
                            var toRemove = services.Where(d => d.ServiceType == typeof(ISender)).ToList();
                            foreach (var d in toRemove) services.Remove(d);

                            services.AddSingleton(senderMock);
                        });
                    });
        }
    }
}
