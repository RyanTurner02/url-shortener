using Url.Shortener.Features.ShortUrls;
using Url.Shortener.Features.ShortUrls.Create;
using Moq;
using Url.Shortener.Features.ShortUrls.Common;

namespace Url.Shortener.Tests.Features.ShortUrls.Create
{
    public sealed class CreateShortUrlCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ReturnsExistingShortenedUrl()
        {
            var originalUrl = "https://example.com";
            var hash = "LliXArW";

            var mockUrlShortenerService = new Mock<IUrlShortenerService>();

            var mockShortUrlRepository = new Mock<IShortUrlRepository>();
            mockShortUrlRepository.Setup(x => x.GetShortUrl(It.IsAny<string>())).ReturnsAsync(hash);

            var createShortUrlCommandHandler = new CreateShortUrlCommandHandler(
                mockUrlShortenerService.Object,
                mockShortUrlRepository.Object);
            var createShortUrlCommand = new CreateShortUrlCommand(originalUrl);

            var actual = await createShortUrlCommandHandler.Handle(createShortUrlCommand, CancellationToken.None);

            Assert.Equal(hash, actual);

            mockUrlShortenerService.Verify(
                x => x.ShortenUrl(
                    It.IsAny<string>()),
                Times.Never);

            mockShortUrlRepository.Verify(
                x => x.AddShortUrl(
                    It.IsAny<ShortUrl>()),
                Times.Never);

            mockShortUrlRepository.Verify(
                x => x.GetShortUrl(
                    It.Is<string>(
                        y => y == originalUrl)),
                Times.Once);
        }

        [Fact]
        public async Task Handle_ReturnsShortenedUrl()
        {
            var originalUrl = "https://example.com/";
            var shortenedUrlHash = "LliXArW";

            var mockUrlShortenerService = new Mock<IUrlShortenerService>();
            mockUrlShortenerService.Setup(x => x.ShortenUrl(It.IsAny<string>())).ReturnsAsync(shortenedUrlHash);
            
            var mockShortUrlRepository = new Mock<IShortUrlRepository>();

            var createShortUrlCommandHandler = new CreateShortUrlCommandHandler(
                mockUrlShortenerService.Object,
                mockShortUrlRepository.Object);
            var createShortUrlCommand = new CreateShortUrlCommand(originalUrl);

            var actual = await createShortUrlCommandHandler.Handle(createShortUrlCommand, CancellationToken.None);

            Assert.Equal(shortenedUrlHash, actual);

            mockUrlShortenerService.Verify(
                x => x.ShortenUrl(
                    It.Is<string>(
                        y => y == originalUrl)),
                Times.Once);

            mockShortUrlRepository.Verify(
                x => x.AddShortUrl(
                    It.Is<ShortUrl>(
                        y => y.OriginalUrl == originalUrl &&
                        y.ShortenedUrl == shortenedUrlHash)),
                Times.Once);

            mockShortUrlRepository.Verify(
                x => x.GetShortUrl(
                    It.Is<string>(
                        y => y == originalUrl)),
                Times.Once);
        }
    }
}
