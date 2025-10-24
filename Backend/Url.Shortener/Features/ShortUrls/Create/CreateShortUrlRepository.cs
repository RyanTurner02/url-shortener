namespace Url.Shortener.Features.ShortUrls.Create
{
    /// <summary>
    /// Repository class for adding short URLs to the database.
    /// </summary>
    public class CreateShortUrlRepository : ICreateShortUrlRepository
    {
        /// <summary>
        /// The short url database context.
        /// </summary>
        private readonly ShortUrlDbContext _shortUrlDbContext;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="shortUrlDbContext">The short url database context.</param>
        public CreateShortUrlRepository(ShortUrlDbContext shortUrlDbContext)
        {
            _shortUrlDbContext = shortUrlDbContext;
        }

        /// <inheritdoc/>
        public async Task<int> AddShortUrl(ShortUrl shortUrl)
        {
            _shortUrlDbContext.ShortUrls.Add(shortUrl);
            return await _shortUrlDbContext.SaveChangesAsync();
        }
    }
}
