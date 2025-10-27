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
        /// Constructor
        /// </summary>
        /// <param name="urlShortenerService">The url shortener service.</param>
        /// <param name="createShortUrlRepository">The create short URL repository.</param>
        public CreateShortUrlCommandHandler(
            IUrlShortenerService urlShortenerService,
            ICreateShortUrlRepository createShortUrlRepository)
        {
            _urlShortenerService = urlShortenerService;
            _createShortUrlRepository = createShortUrlRepository;
        }

        /// <summary>
        /// Handles the request for shortening a URL.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The shortened URL.</returns>
        public async Task<string> Handle(CreateShortUrlCommand request, CancellationToken cancellationToken)
        {
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
