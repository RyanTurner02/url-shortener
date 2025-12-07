using Microsoft.EntityFrameworkCore;
using Url.Shortener.Features.ShortUrls;
using Url.Shortener.Features.ShortUrls.Create;

namespace Url.Shortener.Tests.Features.ShortUrls.Create
{
    public sealed class OriginalUrlRepositoryTests
    {
        [Fact]
        public async Task GetShortUrl_ReturnsNull()
        {
            var databaseName = $"GetShortUrl_{Guid.NewGuid()}";
            var dbContextOptions = new DbContextOptionsBuilder<ShortUrlDbContext>()
                .UseInMemoryDatabase(databaseName)
                .Options;

            await using var shortUrlDbContext = new ShortUrlDbContext(dbContextOptions);
            var originalUrlRepository = new OriginalUrlRepository(shortUrlDbContext);

            var originalUrl = "https://example.com/";
            var actual = await originalUrlRepository.GetShortUrl(originalUrl);

            Assert.Null(actual);
        }

        [Fact]
        public async Task GetShortUrl_ReturnsShortUrl()
        {
            var originalUrl = "https://example.com";
            var expected = "ShortUrl";

            var databaseName = $"GetShortUrl_{Guid.NewGuid()}";
            var dbContextOptions = new DbContextOptionsBuilder<ShortUrlDbContext>()
                .UseInMemoryDatabase(databaseName)
                .Options;

            await using var shortUrlDbContext = new ShortUrlDbContext(dbContextOptions);
            await shortUrlDbContext.ShortUrls.AddAsync(
                new ShortUrl
                {
                    OriginalUrl = originalUrl,
                    ShortenedUrl = expected,
                }
            );
            await shortUrlDbContext.SaveChangesAsync();

            var originalUrlRepository = new OriginalUrlRepository(shortUrlDbContext);
            var actual = await originalUrlRepository.GetShortUrl(originalUrl);
            
            Assert.Equal(expected, actual);
        }
    }
}
