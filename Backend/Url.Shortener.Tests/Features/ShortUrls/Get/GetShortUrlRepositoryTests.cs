using Url.Shortener.Features.ShortUrls;
using Url.Shortener.Features.ShortUrls.Create;
using Url.Shortener.Features.ShortUrls.Get;
using Microsoft.EntityFrameworkCore;

namespace Url.Shortener.Tests.Features.ShortUrls.Get
{
    public sealed class GetShortUrlRepositoryTests
    {
        [Fact]
        public async Task GetShortUrl_ReturnsShortUrlObject()
        {
            var originalUrl = "https://example.com/";
            var shortenedUrlHash = "LliXArW";

            var expected = new ShortUrl
            {
                Id = 1,
                OriginalUrl = originalUrl,
                ShortenedUrl = shortenedUrlHash,
                CreatedAt = DateTime.UtcNow
            };

            var databaseName = $"GetShortUrl_{Guid.NewGuid()}";
            var dbContextOptions = new DbContextOptionsBuilder<ShortUrlDbContext>()
                .UseInMemoryDatabase(databaseName)
                .Options;

            await using var shortUrlDbContext = new ShortUrlDbContext(dbContextOptions);
            var createShortUrlRepository = new CreateShortUrlRepository(shortUrlDbContext);
            var getShortUrlRepository = new GetShortUrlRepository(shortUrlDbContext);
            await createShortUrlRepository.AddShortUrl(expected);

            var actual = await getShortUrlRepository.GetShortUrl(shortenedUrlHash);

            Assert.Equal(expected, actual);
        }
    }
}
