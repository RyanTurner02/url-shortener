namespace Url.Shortener.Features.ShortUrls.Create.Repositories
{
    /// <summary>
    /// The duplicate URL repository class.
    /// </summary>
    public class DuplicateUrlRepository : IDuplicateUrlRepository
    {
        /// <inheritdoc/>
        public Task<bool> DuplicateOriginalUrl(string originalUrl)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<bool> DuplicateShortUrl(string shortUrl)
        {
            throw new NotImplementedException();
        }
    }
}
