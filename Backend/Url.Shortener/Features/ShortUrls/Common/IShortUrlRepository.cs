namespace Url.Shortener.Features.ShortUrls.Common
{
    /// <summary>
    /// Interface for ShortUrl repository operations.
    /// </summary>
    public interface IShortUrlRepository
    {
        /// <summary>
        /// Adds the short URL to the database.
        /// </summary>
        /// <param name="shortUrl">The short url.</param>
        /// <returns>The number of entries written to the database.</returns>
        Task<int> AddShortUrl(ShortUrl shortUrl);

        /// <summary>
        /// Retrieves the shortened URL from the database given an original URL.
        /// </summary>
        /// <param name="originalUrl">The original URL to query.</param>
        /// <returns>The shortened URL.</returns>
        Task<string?> GetShortUrl(string originalUrl);

        /// <summary>
        /// Retrieves the original URL from the database given a short URL.
        /// </summary>
        /// <param name="shortUrl">The short URL to query.</param>
        /// <returns>The original URL.</returns>
        Task<string?> GetOriginalUrl(string shortUrl);

        /// <summary>
        /// Determines whether a given short URL exists.
        /// </summary>
        /// <param name="shortUrl">The short URL to query.</param>
        /// <returns>True if the short URL exists, false otherwise.</returns>
        Task<bool> ShortUrlExists(string shortUrl);
    }
}
