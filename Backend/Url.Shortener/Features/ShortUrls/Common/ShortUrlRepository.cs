using Microsoft.EntityFrameworkCore;

namespace Url.Shortener.Features.ShortUrls.Common
{
    /// <summary>
    /// Class for short url repository operations.
    /// </summary>
    public class ShortUrlRepository : IShortUrlRepository
    {
        /// <summary>
        /// The short url database context.
        /// </summary>
        private readonly ShortUrlDbContext _context;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="context">The short url database context.</param>
        public ShortUrlRepository(ShortUrlDbContext context)
        {
            _context = context;
        }

        /// <inheritdoc/>
        public async Task<int> AddShortUrl(ShortUrl shortUrl)
        {
            _context.ShortUrls.Add(shortUrl);
            return await _context.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task<string?> GetShortUrl(string originalUrl)
        {
            return await _context.ShortUrls
                .Where(x => x.OriginalUrl == originalUrl)
                .Select(x => x.ShortenedUrl)
                .FirstOrDefaultAsync();
        }

        /// <inheritdoc/>
        public async Task<string?> GetOriginalUrl(string shortUrl)
        {
            return await _context.ShortUrls
                .Where(x => x.ShortenedUrl == shortUrl)
                .Select(x => x.OriginalUrl)
                .FirstOrDefaultAsync();
        }

        /// <inheritdoc/>
        public async Task<bool> ShortUrlExists(string shortUrl)
        {
            return await _context.ShortUrls.AnyAsync(x => x.ShortenedUrl == shortUrl);
        }
    }
}
