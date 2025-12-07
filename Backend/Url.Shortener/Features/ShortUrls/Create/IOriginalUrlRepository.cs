namespace Url.Shortener.Features.ShortUrls.Create
{
    /// <summary>
    /// Interface for querying original URLs from the database.
    /// </summary>
    public interface IOriginalUrlRepository
    {
        /// <summary>
        /// Retrieves the shortened URL from the database given an original URL.
        /// </summary>
        /// <param name="originalUrl">The original URL to query.</param>
        /// <returns>The shortened URL.</returns>
        Task<string?> GetShortUrl(string originalUrl);
    }
}
