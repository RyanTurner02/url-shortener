using MediatR;
using Url.Shortener.Features.ShortUrls.Common;

namespace Url.Shortener.Features.ShortUrls.Create
{
    /// <summary>
    /// Handler class for creating short URLs.
    /// </summary>
    public class CreateShortUrlCommandHandler : IRequestHandler<CreateShortUrlCommand, string>
    {
        /// <summary>
        /// The url shortener service.
        /// </summary>
        private readonly IUrlShortenerService _urlShortenerService;

        /// <summary>
        /// The short url repository.
        /// </summary>
        private readonly IShortUrlRepository _shortUrlRepository;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="urlShortenerService">The url shortener service.</param>
        /// <param name="shortUrlRepository">The short URL repository.</param>
        public CreateShortUrlCommandHandler(
            IUrlShortenerService urlShortenerService,
            IShortUrlRepository shortUrlRepository)
        {
            _urlShortenerService = urlShortenerService;
            _shortUrlRepository = shortUrlRepository;
        }

        /// <summary>
        /// Handles the request for shortening a URL.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The shortened URL.</returns>
        public async Task<string> Handle(CreateShortUrlCommand request, CancellationToken cancellationToken)
        {
            var originalUrl = request.Url.Trim();

            var shortUrlDb = await _shortUrlRepository.GetShortUrl(originalUrl);
            if (!string.IsNullOrEmpty(shortUrlDb))
            {
                return shortUrlDb;
            }

            var shortUrl = new ShortUrl
            {
                OriginalUrl = originalUrl,
                ShortenedUrl = await _urlShortenerService.ShortenUrl(originalUrl),
            };

            if (string.IsNullOrEmpty(shortUrl.ShortenedUrl))
            {
                return string.Empty;
            }

            await _shortUrlRepository.AddShortUrl(shortUrl);
            return shortUrl.ShortenedUrl;
        }
    }
}
