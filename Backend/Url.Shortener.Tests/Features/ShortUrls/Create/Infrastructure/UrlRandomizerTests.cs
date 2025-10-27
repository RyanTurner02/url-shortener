using Url.Shortener.Features.ShortUrls.Create.Infrastructure;

namespace Url.Shortener.Tests.Features.ShortUrls.Create.Infrastructure
{
    public sealed class UrlRandomizerTests
    {
        [Fact]
        public void Randomize_ReturnsRandomizedUrl()
        {
            var urlLength = 7;
            var encodedUrl = "BEiD8U92IoYW2ofPhe7GyEBLStEfKccm3G75JyiilanAt8FcsIwpKYWhB2HAZ8CMpN8gZlo5XXHPlYG0HyRslr";

            var urlRandomizer = new UrlRandomizer();
            var actual = urlRandomizer.Randomize(urlLength, encodedUrl);

            Assert.False(string.IsNullOrEmpty(actual));
            Assert.Equal(urlLength, actual.Length);
            Assert.True(actual.All(c => encodedUrl.Contains(c)));
        }
    }
}
