using Moq;
using Url.Shortener.Features.ShortUrls.Common;
using Url.Shortener.Features.ShortUrls.Create;
using Url.Shortener.Features.ShortUrls.Create.Utilities;

namespace Url.Shortener.Tests.Features.ShortUrls.Create
{
    /// <summary>
    /// Tests the <see cref=UrlShortenerService"/> class.
    /// </summary>
    public sealed class UrlShortenerServiceTests
    {
        /// <summary>
        /// Tests the <see cref="UrlShortenerService.ShortenUrl(string)"/> when the original URL is empty.
        /// </summary>
        [Fact]
        public async Task ShortenUrl_EmptyOriginalUrl_ReturnsEmptyString()
        {
            var originalUrl = string.Empty;

            var mockUrlRandomizer = new Mock<IUrlRandomizer>();
            var mockShortUrlRepository = new Mock<IShortUrlRepository>();

            var urlShortenerService = new UrlShortenerService(
                mockUrlRandomizer.Object,
                mockShortUrlRepository.Object);

            var actual = await urlShortenerService.ShortenUrl(originalUrl);

            Assert.True(string.IsNullOrEmpty(actual));

            mockUrlRandomizer.Verify(
                x => x.Randomize(
                    It.IsAny<int>(),
                    It.IsAny<string>()),
                Times.Never);

            mockShortUrlRepository.Verify(
                x => x.ShortUrlExists(
                    It.IsAny<string>()),
                Times.Never);
        }

        /// <summary>
        /// Tests the <see cref="UrlShortenerService.ShortenUrl(string)"/> when the maximum attempts are exceeded.
        /// </summary>
        [Fact]
        public async Task ShortenUrl_MaxAttempts_ReturnsEmptyString()
        {
            var originalUrl = "https://example.com";
            var encodedUrl = "BQ4R18M89B0BpYoVxEOTixQNzxbntfovLW6TraZtiFdNGBsykjkKD2ML9V4EWTz3jVsBDLDUjeBL3JbgeBCyWv";
            var randomizedUrl = "ShortUrl";
            var randomizeLength = 7;

            var mockUrlRandomizer = new Mock<IUrlRandomizer>();
            mockUrlRandomizer.Setup(x => x.Randomize(It.IsAny<int>(), It.IsAny<string>())).Returns(randomizedUrl);

            var mockShortUrlRepository = new Mock<IShortUrlRepository>();
            mockShortUrlRepository.Setup(x => x.ShortUrlExists(It.IsAny<string>())).ReturnsAsync(true);

            var urlShortenerService = new UrlShortenerService(
                mockUrlRandomizer.Object,
                mockShortUrlRepository.Object);

            var actual = await urlShortenerService.ShortenUrl(originalUrl);

            Assert.True(string.IsNullOrEmpty(actual));

            mockUrlRandomizer.Verify(
                x => x.Randomize(
                    It.Is<int>(y => y == randomizeLength),
                    It.Is<string>(y => y == encodedUrl)),
                Times.Exactly(5));

            mockShortUrlRepository.Verify(
                x => x.ShortUrlExists(
                    It.Is<string>(y => y == randomizedUrl)),
                Times.Exactly(5));
        }

        /// <summary>
        /// Tests the <see cref="UrlShortenerService.ShortenUrl(string)"/> method.
        /// </summary>
        [Fact]
        public async Task ShortenUrl_ReturnShortUrl()
        {
            var originalUrl = "https://example.com";
            var encodedUrl = "BQ4R18M89B0BpYoVxEOTixQNzxbntfovLW6TraZtiFdNGBsykjkKD2ML9V4EWTz3jVsBDLDUjeBL3JbgeBCyWv";
            var expected = "ShortUrl";
            var randomizeLength = 7;

            var mockUrlRandomizer = new Mock<IUrlRandomizer>();
            mockUrlRandomizer.Setup(x => x.Randomize(It.IsAny<int>(), It.IsAny<string>())).Returns(expected);

            var mockShortUrlRepository = new Mock<IShortUrlRepository>();
            mockShortUrlRepository.Setup(x => x.ShortUrlExists(It.IsAny<string>())).ReturnsAsync(false);

            var urlShortenerService = new UrlShortenerService(
                mockUrlRandomizer.Object,
                mockShortUrlRepository.Object);

            var actual = await urlShortenerService.ShortenUrl(originalUrl);

            Assert.Equal(expected, actual);

            mockUrlRandomizer.Verify(
                x => x.Randomize(
                    It.Is<int>(y => y == randomizeLength),
                    It.Is<string>(y => y == encodedUrl)),
                Times.Once);

            mockShortUrlRepository.Verify(
                x => x.ShortUrlExists(
                    It.Is<string>(y => y == expected)),
                Times.Once);
        }

        /// <summary>
        /// Tests the <see cref="UrlShortenerService.ShortenUrl(string)"/> method when a duplicate short URL is generated.
        /// </summary>
        [Fact]
        public async Task ShortenUrl_DuplicateHandling_ReturnsUniqueShortUrl()
        {
            var originalUrl = "https://example.com";
            var encodedUrl = "BQ4R18M89B0BpYoVxEOTixQNzxbntfovLW6TraZtiFdNGBsykjkKD2ML9V4EWTz3jVsBDLDUjeBL3JbgeBCyWv";
            var duplicateUrl = "DuplicateUrl";
            var expected = "ShortUrl";
            var randomizeLength = 7;

            var mockUrlRandomizer = new Mock<IUrlRandomizer>();
            mockUrlRandomizer.SetupSequence(
                x => x.Randomize(
                    It.IsAny<int>(),
                    It.IsAny<string>()))
                .Returns(duplicateUrl)
                .Returns(duplicateUrl)
                .Returns(expected);

            var mockShortUrlRepository = new Mock<IShortUrlRepository>();
            mockShortUrlRepository.SetupSequence(
                x => x.ShortUrlExists(
                    It.IsAny<string>()))
                .ReturnsAsync(true)
                .ReturnsAsync(true)
                .ReturnsAsync(false);

            var urlShortenerService = new UrlShortenerService(
                mockUrlRandomizer.Object,
                mockShortUrlRepository.Object);

            var actual = await urlShortenerService.ShortenUrl(originalUrl);

            Assert.Equal(expected, actual);

            mockUrlRandomizer.Verify(
                x => x.Randomize(
                    It.Is<int>(y => y == randomizeLength),
                    It.Is<string>(y => y == encodedUrl)),
                Times.Exactly(3));

            mockShortUrlRepository.Verify(
                x => x.ShortUrlExists(
                    It.Is<string>(y => y == duplicateUrl)),
                Times.Exactly(2));

            mockShortUrlRepository.Verify(
                x => x.ShortUrlExists(
                    It.Is<string>(y => y == expected)),
                Times.Once);
        }
    }
}
