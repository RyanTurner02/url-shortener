namespace Backend.Features.ShortUrls.Create
{
    /// <summary>
    /// Interface for adding short URLs to the database.
    /// </summary>
    public interface ICreateShortUrlRepository
    {
        /// <summary>
        /// Adds the short URL to the database.
        /// </summary>
        /// <param name="shortUrl">The short url</param>
        /// <returns>The number of entries written to the database.</returns>
        Task<int> AddShortUrl(ShortUrl shortUrl);
    }
}
