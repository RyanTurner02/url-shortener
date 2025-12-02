using Url.Shortener.Features.ShortUrls.Create.Infrastructure;

namespace Url.Shortener.Tests.Features.ShortUrls.Create.Infrastructure
{
    public sealed class UrlEncoderTests
    {
        [Fact]
        public void Encode_ReturnsEncodedUrl()
        {
            var hash = "0f115db062b7c0dd030b16878c99dea5c354b49dc37b38eb8846179c7783e9d7";
            var expected = "BEiD8U92IoYW2ofPhe7GyEBLStEfKccm3G75JyiilanAt8FcsIwpKYWhB2HAZ8CMpN8gZlo5XXHPlYG0HyRslr";

            var actual = UrlEncoder.Encode(hash);

            Assert.Equal(expected, actual);
        }
    }
}
