namespace Url.Shortener.Features.ShortUrls.Create
{
    /// <summary>
    /// The url shortener service interface.
    /// </summary>
    public interface IUrlShortenerService
    {
        /// <summary>
        /// Shortens the original url.
        /// </summary>
        /// <param name="originalUrl">The original url.</param>
        /// <returns>The shortened url.</returns>
        string ShortenUrl(string originalUrl);
    }
}
