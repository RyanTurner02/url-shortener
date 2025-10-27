using Url.Shortener.Features.ShortUrls.Create.Infrastructure;

namespace Url.Shortener.Features.ShortUrls.Create
{
    /// <summary>
    /// The url shortener service class.
    /// </summary>
    public class UrlShortenerService : IUrlShortenerService
    {
        /// <summary>
        /// The URL Hasher.
        /// </summary>
        private readonly IUrlHasher _urlHasher;

        /// <summary>
        /// The URL Encoder.
        /// </summary>
        private readonly IUrlEncoder _urlEncoder;

        /// <summary>
        /// The URL Randomizer.
        /// </summary>
        private readonly IUrlRandomizer _urlRandomizer;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="urlHasher">The URL Hasher.</param>
        /// <param name="urlEncoder">The URL Encoder.</param>
        /// <param name="urlRandomizer">The URL Randomizer.</param>
        public UrlShortenerService(
            IUrlHasher urlHasher,
            IUrlEncoder urlEncoder,
            IUrlRandomizer urlRandomizer)
        {
            _urlHasher = urlHasher;
            _urlEncoder = urlEncoder;
            _urlRandomizer = urlRandomizer;
        }

        /// <inheritdoc/>
        public string ShortenUrl(string originalUrl)
        {
            if (string.IsNullOrEmpty(originalUrl))
            {
                return string.Empty;
            }

            var hashedUrl = _urlHasher.Hash(originalUrl);
            var encodedUrl = _urlEncoder.Encode(hashedUrl);

            var shortUrlLength = 7;
            return _urlRandomizer.Randomize(shortUrlLength, encodedUrl);
        }
    }
}
