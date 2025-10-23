namespace Url.Shortener.Features.ShortUrls.Get
{
    /// <summary>
    /// Interface for getting short URL entries from the database.
    /// </summary>
    public interface IGetShortUrlRepository
    {
        /// <summary>
        /// Retrieves a short URL entry from the database.
        /// </summary>
        /// <param name="shortUrl">The short URL.</param>
        /// <returns>The short URL database entry.</returns>
        Task<ShortUrl?> GetShortUrl(string shortUrl);
    }
}
