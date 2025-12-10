using Url.Shortener.Features.ShortUrls.Get;
using Moq;
using Url.Shortener.Features.ShortUrls.Common;

namespace Url.Shortener.Tests.Features.ShortUrls.Get
{
    public sealed class GetShortUrlQueryHandlerTests
    {
        [Fact]
        public async Task Handle_ReturnsOriginalUrl()
        {
            var originalUrl = "https://example.com/";
            var shortenedUrlHash = "LliXArW";

            var mockShortUrlRepository = new Mock<IShortUrlRepository>();
            mockShortUrlRepository.Setup(s =>
                s.GetOriginalUrl(It.IsAny<string>()))
                .ReturnsAsync(originalUrl);

            var getShortUrlQueryHandler = new GetShortUrlQueryHandler(mockShortUrlRepository.Object);
            var getShortUrlQuery = new GetShortUrlQuery(shortenedUrlHash);

            var actual = await getShortUrlQueryHandler.Handle(getShortUrlQuery, CancellationToken.None);

            Assert.Equal(originalUrl, actual);
            mockShortUrlRepository.Verify(x => x.GetOriginalUrl(It.Is<string>(y => y == shortenedUrlHash)), Times.Once);
        }
    }
}
