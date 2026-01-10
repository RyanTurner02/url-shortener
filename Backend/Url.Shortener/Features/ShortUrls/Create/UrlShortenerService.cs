using Url.Shortener.Features.ShortUrls.Common;
using Url.Shortener.Features.ShortUrls.Create.Utilities;

namespace Url.Shortener.Features.ShortUrls.Create
{
    /// <summary>
    /// The url shortener service class.
    /// </summary>
    public class UrlShortenerService : IUrlShortenerService
    {
        /// <summary>
        /// The length of the short URL.
        /// </summary>
        private const int SHORT_URL_LENGTH = 7;

        /// <summary>
        /// The maximum attempts to generate the short URL.
        /// </summary>
        private const int MAX_ATTEMPTS = 5;

        /// <summary>
        /// The url randomizer.
        /// </summary>
        private IUrlRandomizer _urlRandomizer;

        /// <summary>
        /// The short url repository.
        /// </summary>
        private IShortUrlRepository _shortUrlRepository;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="urlRandomizer">The url randomizer.</param>
        /// <param name="shortUrlRepository">The short url repository.</param>
        public UrlShortenerService(
            IUrlRandomizer urlRandomizer,
            IShortUrlRepository shortUrlRepository)
        {
            _urlRandomizer = urlRandomizer;
            _shortUrlRepository = shortUrlRepository;
        }

        /// <inheritdoc/>
        public async Task<string> ShortenUrl(string originalUrl)
        {
            if (string.IsNullOrEmpty(originalUrl))
            {
                return string.Empty;
            }

            var hashedUrl = UrlHasher.Hash(originalUrl);
            var encodedUrl = UrlEncoder.Encode(hashedUrl);
            var randomizedUrl = string.Empty;

            for (var currentAttempt = 0; currentAttempt < MAX_ATTEMPTS; currentAttempt++)
            {
                randomizedUrl = _urlRandomizer.Randomize(
                    SHORT_URL_LENGTH,
                    encodedUrl);

                if (!await _shortUrlRepository.ShortUrlExists(randomizedUrl))
                {
                    return randomizedUrl;
                }
            }

            return string.Empty;
        }
    }
}
