namespace Url.Shortener.Features.ShortUrls.Create.Infrastructure
{
    /// <summary>
    /// The URL hasher interface.
    /// </summary>
    public interface IUrlHasher
    {
        /// <summary>
        /// Hashes the original URL using the SHA-256 hash function.
        /// </summary>
        /// <param name="originalUrl">The original URL.</param>
        /// <returns>The SHA-256 hash of the original URL.</returns>
        string Hash(string originalUrl);
    }
}
