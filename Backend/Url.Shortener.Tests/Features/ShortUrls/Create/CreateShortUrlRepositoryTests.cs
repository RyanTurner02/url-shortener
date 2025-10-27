using Url.Shortener.Features.ShortUrls;
using Url.Shortener.Features.ShortUrls.Create;
using Microsoft.EntityFrameworkCore;

namespace Url.Shortener.Tests.Features.ShortUrls.Create
{
    public sealed class CreateShortUrlRepositoryTests
    {
        [Fact]
        public async Task AddShortUrl_ReturnsOneEntry()
        {
            var databaseName = $"AddShortUrl_{Guid.NewGuid()}";
            var dbContextOptions = new DbContextOptionsBuilder<ShortUrlDbContext>()
                .UseInMemoryDatabase(databaseName)
                .Options;

            await using var shortUrlDbContext = new ShortUrlDbContext(dbContextOptions);
            var createShortUrlRepository = new CreateShortUrlRepository(shortUrlDbContext);

            var shortUrl = new ShortUrl
            {
                OriginalUrl = "https://example.com/",
                ShortenedUrl = "LliXArW",
            };

            var expected = 1;
            var actual = await createShortUrlRepository.AddShortUrl(shortUrl);

            Assert.Equal(expected, actual);
            Assert.Single(shortUrlDbContext.ShortUrls);

            var storedShortUrl = await shortUrlDbContext.ShortUrls.SingleAsync();
            
            Assert.Equal(shortUrl.Id, storedShortUrl.Id);
            Assert.Equal(shortUrl.OriginalUrl, storedShortUrl.OriginalUrl);
            Assert.Equal(shortUrl.ShortenedUrl, storedShortUrl.ShortenedUrl);
            Assert.Equal(shortUrl.CreatedAt, storedShortUrl.CreatedAt);
        }
    }
}
