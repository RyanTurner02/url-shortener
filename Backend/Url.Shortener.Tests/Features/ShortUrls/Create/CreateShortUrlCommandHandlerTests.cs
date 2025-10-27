using Url.Shortener.Features.ShortUrls;
using Url.Shortener.Features.ShortUrls.Create;
using Moq;

namespace Url.Shortener.Tests.Features.ShortUrls.Create
{
    public sealed class CreateShortUrlCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ReturnsShortenedUrl()
        {
            var originalUrl = "https://example.com/";
            var shortenedUrlHash = "LliXArW";

            var mockService = new Mock<IUrlShortenerService>();
            mockService.Setup(x => x.ShortenUrl(It.IsAny<string>())).Returns(shortenedUrlHash);
            
            var mockRepository = new Mock<ICreateShortUrlRepository>();

            var createShortUrlCommandHandler = new CreateShortUrlCommandHandler(mockService.Object, mockRepository.Object);
            var createShortUrlCommand = new CreateShortUrlCommand(originalUrl);

            var actual = await createShortUrlCommandHandler.Handle(createShortUrlCommand, CancellationToken.None);

            Assert.Equal(shortenedUrlHash, actual);

            mockService.Verify(
                x => x.ShortenUrl(
                    It.Is<string>(
                        y => y == originalUrl)),
                Times.Once);

            mockRepository.Verify(
                x => x.AddShortUrl(
                    It.Is<ShortUrl>(
                        y => y.OriginalUrl == originalUrl &&
                        y.ShortenedUrl == shortenedUrlHash)),
                Times.Once);
        }
    }
}
