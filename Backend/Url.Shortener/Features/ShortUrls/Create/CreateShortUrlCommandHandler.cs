using MediatR;

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
        /// The create short URL repository.
        /// </summary>
        private readonly ICreateShortUrlRepository _createShortUrlRepository;

        /// <summary>
        /// The original URL repository.
        /// </summary>
        private readonly IOriginalUrlRepository _originalUrlRepository;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="urlShortenerService">The url shortener service.</param>
        /// <param name="createShortUrlRepository">The create short URL repository.</param>
        /// <param name="originalUrlRepository">The original URL repository.</param>
        public CreateShortUrlCommandHandler(
            IUrlShortenerService urlShortenerService,
            ICreateShortUrlRepository createShortUrlRepository,
            IOriginalUrlRepository originalUrlRepository)
        {
            _urlShortenerService = urlShortenerService;
            _createShortUrlRepository = createShortUrlRepository;
            _originalUrlRepository = originalUrlRepository;
        }

        /// <summary>
        /// Handles the request for shortening a URL.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The shortened URL.</returns>
        public async Task<string> Handle(CreateShortUrlCommand request, CancellationToken cancellationToken)
        {
            var originalUrlDb = await _originalUrlRepository.GetShortUrl(request.Url);
            if (!string.IsNullOrEmpty(originalUrlDb))
            {
                return originalUrlDb;
            }

            var shortUrl = new ShortUrl
            {
                OriginalUrl = request.Url,
                ShortenedUrl = _urlShortenerService.ShortenUrl(request.Url),
            };

            await _createShortUrlRepository.AddShortUrl(shortUrl);

            return shortUrl.ShortenedUrl;
        }
    }
}
