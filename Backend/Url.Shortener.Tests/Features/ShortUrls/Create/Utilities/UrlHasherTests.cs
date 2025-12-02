using Url.Shortener.Features.ShortUrls.Create.Utilities;

namespace Url.Shortener.Tests.Features.ShortUrls.Create.Utilities
{
    public sealed class UrlHasherTests
    {
        [Fact]
        public void Hash_ReturnsHashedUrl()
        {
            var originalUrl = "https://example.com/";
            var expected = "0f115db062b7c0dd030b16878c99dea5c354b49dc37b38eb8846179c7783e9d7";

            var actual = UrlHasher.Hash(originalUrl);

            Assert.Equal(expected, actual);
        }
    }
}
