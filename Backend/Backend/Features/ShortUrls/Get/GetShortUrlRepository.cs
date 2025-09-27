using Microsoft.EntityFrameworkCore;

namespace Backend.Features.ShortUrls.Get
{
    /// <summary>
    /// Repository class for getting short URL entries from the database.
    /// </summary>
    public class GetShortUrlRepository : IGetShortUrlRepository
    {
        /// <summary>
        /// The short URL database context.
        /// </summary>
        private readonly ShortUrlDbContext _context;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="context">The short URL database context.</param>
        public GetShortUrlRepository(ShortUrlDbContext context)
        {
            _context = context;
        }

        /// <inheritdoc/>
        public async Task<ShortUrl?> GetShortUrl(string shortUrl)
        {
            return await _context.ShortUrls
                .Where(x => x.ShortenedUrl == shortUrl)
                .FirstOrDefaultAsync();
        }
    }
}
