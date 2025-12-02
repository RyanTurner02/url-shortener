using Url.Shortener.Features.ShortUrls.Create.Infrastructure;

namespace Url.Shortener.Features.ShortUrls.Create
{
    /// <summary>
    /// The url shortener service class.
    /// </summary>
    public class UrlShortenerService : IUrlShortenerService
    {
        /// <inheritdoc/>
        public string ShortenUrl(string originalUrl)
        {
            if (string.IsNullOrEmpty(originalUrl))
            {
                return string.Empty;
            }

            var hashedUrl = UrlHasher.Hash(originalUrl);
            var encodedUrl = UrlEncoder.Encode(hashedUrl);

            var shortUrlLength = 7;
            return UrlRandomizer.Randomize(shortUrlLength, encodedUrl);
        }
    }
}
