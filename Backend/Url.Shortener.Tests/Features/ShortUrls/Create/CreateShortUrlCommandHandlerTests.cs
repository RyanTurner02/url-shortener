using Url.Shortener.Features.ShortUrls;
using Url.Shortener.Features.ShortUrls.Create;
using Moq;

namespace Url.Shortener.Tests.Features.ShortUrls.Create
{
    public sealed class CreateShortUrlCommandHandlerTests
    {
        [Theory]
        [InlineData("https://www.google.com/", "google.com/")]
        [InlineData("https://www.youtube.com/", "youtube.com/")]
        public async Task Handle_ReturnsShortenedUrl(string originalUrl, string shortenedUrl)
        {
            var mockService = new Mock<IUrlShortenerService>();
            var mockRepository = new Mock<ICreateShortUrlRepository>();
            var createShortUrlCommandHandler = new CreateShortUrlCommandHandler(mockService.Object, mockRepository.Object);
            var createShortUrlCommand = new CreateShortUrlCommand(originalUrl);

            var actual = await createShortUrlCommandHandler.Handle(createShortUrlCommand, CancellationToken.None);

            Assert.Equal(shortenedUrl, actual);
            mockRepository.Verify(
                x => x.AddShortUrl(
                    It.Is<ShortUrl>(
                        y => y.OriginalUrl == originalUrl &&
                        y.ShortenedUrl == shortenedUrl)),
                Times.Once);
        }
    }
}
