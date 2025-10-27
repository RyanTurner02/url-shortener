using Url.Shortener.Features.ShortUrls;
using Url.Shortener.Features.ShortUrls.Get;
using Moq;

namespace Url.Shortener.Tests.Features.ShortUrls.Get
{
    public sealed class GetShortUrlQueryHandlerTests
    {
        [Fact]
        public async Task Handle_ReturnsOriginalUrl()
        {
            var originalUrl = "https://example.com/";
            var shortenedUrlHash = "LliXArW";

            var shortUrl = new ShortUrl
            {
                Id = 1,
                OriginalUrl = originalUrl,
                ShortenedUrl = shortenedUrlHash,
                CreatedAt = DateTime.UtcNow
            };

            var mockRepository = new Mock<IGetShortUrlRepository>();
            mockRepository.Setup(s =>
                s.GetShortUrl(It.IsAny<string>()))
                .ReturnsAsync(shortUrl);

            var getShortUrlQueryHandler = new GetShortUrlQueryHandler(mockRepository.Object);
            var getShortUrlQuery = new GetShortUrlQuery(shortenedUrlHash);

            var actual = await getShortUrlQueryHandler.Handle(getShortUrlQuery, CancellationToken.None);

            Assert.Equal(originalUrl, actual);
            mockRepository.Verify(x => x.GetShortUrl(It.Is<string>(y => y == shortenedUrlHash)), Times.Once);
        }
    }
}
