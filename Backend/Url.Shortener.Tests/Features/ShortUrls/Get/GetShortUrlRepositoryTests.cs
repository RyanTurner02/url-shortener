using Backend.Features.ShortUrls;
using Backend.Features.ShortUrls.Create;
using Backend.Features.ShortUrls.Get;
using Microsoft.EntityFrameworkCore;

namespace Url.Shortener.Tests.Features.ShortUrls.Get
{
    public sealed class GetShortUrlRepositoryTests
    {
        [Fact]
        public async Task GetShortUrl_ShortenedUrl_ReturnsShortUrlObject()
        {
            var originalUrl = "https://youtube.com";
            var shortenedUrl = "youtube.com";

            var expected = new ShortUrl
            {
                Id = 1,
                OriginalUrl = originalUrl,
                ShortenedUrl = shortenedUrl,
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

            var actual = await getShortUrlRepository.GetShortUrl(shortenedUrl);

            Assert.Equal(expected, actual);
        }
    }
}
