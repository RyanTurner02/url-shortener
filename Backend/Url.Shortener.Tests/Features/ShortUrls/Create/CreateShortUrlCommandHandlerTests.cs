using Url.Shortener.Features.ShortUrls;
using Url.Shortener.Features.ShortUrls.Create;
using Moq;

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

            var mockCreateShortUrlRepository = new Mock<ICreateShortUrlRepository>();

            var mockOriginalUrlRepository = new Mock<IOriginalUrlRepository>();
            mockOriginalUrlRepository.Setup(x => x.GetShortUrl(It.IsAny<string>())).ReturnsAsync(hash);

            var createShortUrlCommandHandler = new CreateShortUrlCommandHandler(
                mockUrlShortenerService.Object,
                mockCreateShortUrlRepository.Object,
                mockOriginalUrlRepository.Object);
            var createShortUrlCommand = new CreateShortUrlCommand(originalUrl);

            var actual = await createShortUrlCommandHandler.Handle(createShortUrlCommand, CancellationToken.None);

            Assert.Equal(hash, actual);

            mockUrlShortenerService.Verify(
                x => x.ShortenUrl(
                    It.IsAny<string>()),
                Times.Never);

            mockCreateShortUrlRepository.Verify(
                x => x.AddShortUrl(
                    It.IsAny<ShortUrl>()),
                Times.Never);

            mockOriginalUrlRepository.Verify(
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
            mockUrlShortenerService.Setup(x => x.ShortenUrl(It.IsAny<string>())).Returns(shortenedUrlHash);
            
            var mockCreateShortUrlRepositoryRepository = new Mock<ICreateShortUrlRepository>();
            var mockOriginalUrlRepository = new Mock<IOriginalUrlRepository>();

            var createShortUrlCommandHandler = new CreateShortUrlCommandHandler(
                mockUrlShortenerService.Object,
                mockCreateShortUrlRepositoryRepository.Object,
                mockOriginalUrlRepository.Object);
            var createShortUrlCommand = new CreateShortUrlCommand(originalUrl);

            var actual = await createShortUrlCommandHandler.Handle(createShortUrlCommand, CancellationToken.None);

            Assert.Equal(shortenedUrlHash, actual);

            mockUrlShortenerService.Verify(
                x => x.ShortenUrl(
                    It.Is<string>(
                        y => y == originalUrl)),
                Times.Once);

            mockCreateShortUrlRepositoryRepository.Verify(
                x => x.AddShortUrl(
                    It.Is<ShortUrl>(
                        y => y.OriginalUrl == originalUrl &&
                        y.ShortenedUrl == shortenedUrlHash)),
                Times.Once);

            mockOriginalUrlRepository.Verify(
                x => x.GetShortUrl(
                    It.Is<string>(
                        y => y == originalUrl)),
                Times.Once);
        }
    }
}
