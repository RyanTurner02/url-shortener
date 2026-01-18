using Microsoft.EntityFrameworkCore;
using Url.Shortener.Features.ShortUrls;
using Url.Shortener.Features.ShortUrls.Common;
using Url.Shortener.Tests.Infrastructure;

namespace Url.Shortener.Tests.Features.ShortUrls.Common
{
    /// <summary>
    /// Tests the <see cref="ShortUrlRepository"/> class.
    /// </summary>
    public sealed class ShortUrlRepositoryTests : IClassFixture<MySqlContainerFixture>
    {
        /// <summary>
        /// The MySQL test container fixture.
        /// </summary>
        private readonly MySqlContainerFixture _fixture;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="fixture">The MySQL test container fixture.</param>
        public ShortUrlRepositoryTests(MySqlContainerFixture fixture)
        {
            _fixture = fixture;
        }

        /// <summary>
        /// Tests the <see cref="ShortUrlRepository.AddShortUrl(ShortUrl)"/> method.
        /// </summary>
        [Fact]
        public async Task AddShortUrl_ReturnsOneEntry()
        {
            await using var db = _fixture.CreateShortUrlDbContext();
            var shortUrlRepository = new ShortUrlRepository(db);

            var shortUrl = new ShortUrl
            {
                OriginalUrl = "https://example.com/",
                ShortenedUrl = "LliXArW",
            };

            var expected = 1;
            var actual = await shortUrlRepository.AddShortUrl(shortUrl);

            Assert.Equal(expected, actual);
            Assert.Single(db.ShortUrls);

            var storedShortUrl = await db.ShortUrls.SingleAsync();

            Assert.Equal(shortUrl.Id, storedShortUrl.Id);
            Assert.Equal(shortUrl.OriginalUrl, storedShortUrl.OriginalUrl);
            Assert.Equal(shortUrl.ShortenedUrl, storedShortUrl.ShortenedUrl);
            Assert.Equal(shortUrl.CreatedAt, storedShortUrl.CreatedAt);

            await db.Database.EnsureDeletedAsync();
            await db.Database.MigrateAsync();
        }

        /// <summary>
        /// Tests the <see cref="ShortUrlRepository.GetShortUrl(string)"/> method when no entry exists.
        /// </summary>
        [Fact]
        public async Task GetShortUrl_ReturnsNull()
        {
            await using var db = _fixture.CreateShortUrlDbContext();
            var shortUrlRepository = new ShortUrlRepository(db);

            var originalUrl = "https://example.com/";
            var actual = await shortUrlRepository.GetShortUrl(originalUrl);

            Assert.Null(actual);

            await db.Database.EnsureDeletedAsync();
            await db.Database.MigrateAsync();
        }

        /// <summary>
        /// Tests the <see cref="ShortUrlRepository.GetShortUrl(string)"/> method when an entry exists.
        /// </summary>
        [Fact]
        public async Task GetShortUrl_ReturnsShortUrl()
        {
            var originalUrl = "https://example.com";
            var expected = "ShortUrl";

            await using var db = _fixture.CreateShortUrlDbContext();
            await db.ShortUrls.AddAsync(
                new ShortUrl
                {
                    OriginalUrl = originalUrl,
                    ShortenedUrl = expected,
                }
            );
            await db.SaveChangesAsync();

            var shortUrlRepository = new ShortUrlRepository(db);
            var actual = await shortUrlRepository.GetShortUrl(originalUrl);

            Assert.Equal(expected, actual);

            await db.Database.EnsureDeletedAsync();
            await db.Database.MigrateAsync();
        }

        /// <summary>
        /// Tests the <see cref="ShortUrlRepository.GetOriginalUrl(string)"/> method when no entry exists.
        /// </summary>
        [Fact]
        public async Task GetOriginalUrl_ReturnsNull()
        {
            var shortUrl = "ShortUrl";

            await using var db = _fixture.CreateShortUrlDbContext();
            var shortUrlRepository = new ShortUrlRepository(db);

            var actual = await shortUrlRepository.GetOriginalUrl(shortUrl);

            Assert.Null(actual);

            await db.Database.EnsureDeletedAsync();
            await db.Database.MigrateAsync();
        }

        /// <summary>
        /// Tests the <see cref="ShortUrlRepository.GetOriginalUrl(string)"/> method when an entry exists.
        /// </summary>
        [Fact]
        public async Task GetOriginalUrl_ReturnsOriginalUrl()
        {
            string expected = "https://example.com";
            string shortUrl = "ShortUrl";

            await using var db = _fixture.CreateShortUrlDbContext();
            await db.ShortUrls.AddAsync(
                new ShortUrl
                {
                    OriginalUrl = expected,
                    ShortenedUrl = shortUrl,
                }
            );
            await db.SaveChangesAsync();

            var shortUrlRepository = new ShortUrlRepository(db);
            var actual = await shortUrlRepository.GetOriginalUrl(shortUrl);

            Assert.Equal(expected, actual);

            await db.Database.EnsureDeletedAsync();
            await db.Database.MigrateAsync();
        }

        /// <summary>
        /// Tests the <see cref="ShortUrlRepository.ShortUrlExists(string)"/> method when no entry exists.
        /// </summary>
        [Fact]
        public async Task ShortUrlExists_ReturnsFalse()
        {
            var shortUrl = "ShortUrl";

            await using var db = _fixture.CreateShortUrlDbContext();
            var shortUrlRepository = new ShortUrlRepository(db);
            
            var actual = await shortUrlRepository.ShortUrlExists(shortUrl);

            Assert.False(actual);

            await db.Database.EnsureDeletedAsync();
            await db.Database.MigrateAsync();
        }

        /// <summary>
        /// Tests the <see cref="ShortUrlRepository.ShortUrlExists(string)"/> method when an entry exists.
        /// </summary>
        [Fact]
        public async Task ShortUrlExists_ReturnsTrue()
        {
            var shortUrl = "ShortUrl";

            await using var db = _fixture.CreateShortUrlDbContext();
            await db.ShortUrls.AddAsync(
                new ShortUrl
                {
                    OriginalUrl = string.Empty,
                    ShortenedUrl = shortUrl,
                }
            );
            await db.SaveChangesAsync();

            var shortUrlRepository = new ShortUrlRepository(db);
            var actual = await shortUrlRepository.ShortUrlExists(shortUrl);

            Assert.True(actual);

            await db.Database.EnsureDeletedAsync();
            await db.Database.MigrateAsync();
        }
    }
}
