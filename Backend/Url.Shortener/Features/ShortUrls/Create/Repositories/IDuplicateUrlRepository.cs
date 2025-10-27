namespace Url.Shortener.Features.ShortUrls.Create.Repositories
{
    /// <summary>
    /// The duplicate URL repository interface.
    /// </summary>
    public interface IDuplicateUrlRepository
    {
        /// <summary>
        /// Checks if there is a duplicate short URL in the database.
        /// </summary>
        /// <param name="shortUrl">The short URL.</param>
        /// <returns>True if there is a duplicate short URL. False otherwise.</returns>
        Task<bool> DuplicateShortUrl(string shortUrl);

        /// <summary>
        /// Checks if there is a duplicate original URL in the database.
        /// </summary>
        /// <param name="originalUrl">The original URL.</param>
        /// <returns>True if there is a duplicate original URL. False otherwise.</returns>
        Task<bool> DuplicateOriginalUrl(string originalUrl);
    }
}
