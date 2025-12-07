using Microsoft.EntityFrameworkCore;

namespace Url.Shortener.Features.ShortUrls.Create
{
    /// <summary>
    /// Repository class for querying original URLs from the database.
    /// </summary>
    public class OriginalUrlRepository : IOriginalUrlRepository
    {
        /// <summary>
        /// The short URL database context.
        /// </summary>
        private readonly ShortUrlDbContext _shortUrlDbContext;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="shortUrlDbContext">The short URL database context.</param>
        public OriginalUrlRepository(ShortUrlDbContext shortUrlDbContext)
        {
            _shortUrlDbContext = shortUrlDbContext;
        }

        /// <inheritdoc/>
        public async Task<string?> GetShortUrl(string originalUrl)
        {
            return await _shortUrlDbContext.ShortUrls
                .Where(x => x.OriginalUrl == originalUrl)
                .Select(x => x.ShortenedUrl)
                .FirstOrDefaultAsync();
        }
    }
}
