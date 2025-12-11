using Microsoft.EntityFrameworkCore;
using Url.Shortener.Features.ShortUrls;
using Url.Shortener.Features.ShortUrls.Common;

namespace Url.Shortener.Tests.Features.ShortUrls.Common
{
    /// <summary>
    /// Tests the <see cref="ShortUrlRepository"/> class.
    /// </summary>
    public sealed class ShortUrlRepositoryTests
    {
        /// <summary>
        /// Tests the <see cref="ShortUrlRepository.AddShortUrl(ShortUrl)"/> method.
        /// </summary>
        [Fact]
        public async Task AddShortUrl_ReturnsOneEntry()
        {
            var databaseName = $"AddShortUrl_{Guid.NewGuid()}";
            var dbContextOptions = new DbContextOptionsBuilder<ShortUrlDbContext>()
                .UseInMemoryDatabase(databaseName)
                .Options;

            await using var shortUrlDbContext = new ShortUrlDbContext(dbContextOptions);
            var shortUrlRepository = new ShortUrlRepository(shortUrlDbContext);

            var shortUrl = new ShortUrl
            {
                OriginalUrl = "https://example.com/",
                ShortenedUrl = "LliXArW",
            };

            var expected = 1;
            var actual = await shortUrlRepository.AddShortUrl(shortUrl);

            Assert.Equal(expected, actual);
            Assert.Single(shortUrlDbContext.ShortUrls);

            var storedShortUrl = await shortUrlDbContext.ShortUrls.SingleAsync();

            Assert.Equal(shortUrl.Id, storedShortUrl.Id);
            Assert.Equal(shortUrl.OriginalUrl, storedShortUrl.OriginalUrl);
            Assert.Equal(shortUrl.ShortenedUrl, storedShortUrl.ShortenedUrl);
            Assert.Equal(shortUrl.CreatedAt, storedShortUrl.CreatedAt);
        }

        /// <summary>
        /// Tests the <see cref="ShortUrlRepository.GetShortUrl(string)"/> method when no entry exists.
        /// </summary>
        [Fact]
        public async Task GetShortUrl_ReturnsNull()
        {
            var databaseName = $"GetShortUrl_{Guid.NewGuid()}";
            var dbContextOptions = new DbContextOptionsBuilder<ShortUrlDbContext>()
                .UseInMemoryDatabase(databaseName)
                .Options;

            await using var shortUrlDbContext = new ShortUrlDbContext(dbContextOptions);
            var shortUrlRepository = new ShortUrlRepository(shortUrlDbContext);

            var originalUrl = "https://example.com/";
            var actual = await shortUrlRepository.GetShortUrl(originalUrl);

            Assert.Null(actual);
        }

        /// <summary>
        /// Tests the <see cref="ShortUrlRepository.GetShortUrl(string)"/> method when an entry exists.
        /// </summary>
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

            var shortUrlRepository = new ShortUrlRepository(shortUrlDbContext);
            var actual = await shortUrlRepository.GetShortUrl(originalUrl);

            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Tests the <see cref="ShortUrlRepository.GetOriginalUrl(string)"/> method when no entry exists.
        /// </summary>
        [Fact]
        public async Task GetOriginalUrl_ReturnsNull()
        {
            var shortUrl = "ShortUrl";

            var databaseName = $"GetOriginalUrl_{Guid.NewGuid()}";
            var dbContextOptions = new DbContextOptionsBuilder<ShortUrlDbContext>()
                .UseInMemoryDatabase(databaseName)
                .Options;

            await using var shortUrlDbContext = new ShortUrlDbContext(dbContextOptions);
            var shortUrlRepository = new ShortUrlRepository(shortUrlDbContext);

            var actual = await shortUrlRepository.GetOriginalUrl(shortUrl);

            Assert.Null(actual);
        }

        /// <summary>
        /// Tests the <see cref="ShortUrlRepository.GetOriginalUrl(string)"/> method when an entry exists.
        /// </summary>
        [Fact]
        public async Task GetOriginalUrl_ReturnsOriginalUrl()
        {
            string expected = "https://example.com";
            string shortUrl = "ShortUrl";

            var databaseName = $"GetOriginalUrl_{Guid.NewGuid()}";
            var dbContextOptions = new DbContextOptionsBuilder<ShortUrlDbContext>()
                .UseInMemoryDatabase(databaseName)
                .Options;

            await using var shortUrlDbContext = new ShortUrlDbContext(dbContextOptions);
            await shortUrlDbContext.ShortUrls.AddAsync(
                new ShortUrl
                {
                    OriginalUrl = expected,
                    ShortenedUrl = shortUrl,
                }
            );
            await shortUrlDbContext.SaveChangesAsync();

            var shortUrlRepository = new ShortUrlRepository(shortUrlDbContext);
            var actual = await shortUrlRepository.GetOriginalUrl(shortUrl);

            Assert.Equal(expected, actual);
        }

        /// <summary>
        /// Tests the <see cref="ShortUrlRepository.ShortUrlExists(string)"/> method when no entry exists.
        /// </summary>
        [Fact]
        public async Task ShortUrlExists_ReturnsFalse()
        {
            var shortUrl = "ShortUrl";

            var databaseName = $"ShortUrlExists_{Guid.NewGuid()}";
            var dbContextOptions = new DbContextOptionsBuilder<ShortUrlDbContext>()
                .UseInMemoryDatabase(databaseName)
                .Options;

            await using var shortUrlDbContext = new ShortUrlDbContext(dbContextOptions);
            var shortUrlRepository = new ShortUrlRepository(shortUrlDbContext);
            
            var actual = await shortUrlRepository.ShortUrlExists(shortUrl);

            Assert.False(actual);
        }

        /// <summary>
        /// Tests the <see cref="ShortUrlRepository.ShortUrlExists(string)"/> method when an entry exists.
        /// </summary>
        [Fact]
        public async Task ShortUrlExists_ReturnsTrue()
        {
            var shortUrl = "ShortUrl";

            var databaseName = $"ShortUrlExists_{Guid.NewGuid()}";
            var dbContextOptions = new DbContextOptionsBuilder<ShortUrlDbContext>()
                .UseInMemoryDatabase(databaseName)
                .Options;

            await using var shortUrlDbContext = new ShortUrlDbContext(dbContextOptions);
            await shortUrlDbContext.ShortUrls.AddAsync(
                new ShortUrl
                {
                    OriginalUrl = string.Empty,
                    ShortenedUrl = shortUrl,
                }
            );
            await shortUrlDbContext.SaveChangesAsync();

            var shortUrlRepository = new ShortUrlRepository(shortUrlDbContext);
            var actual = await shortUrlRepository.ShortUrlExists(shortUrl);

            Assert.True(actual);
        }
    }
}
