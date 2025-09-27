using MediatR;

namespace Backend.Features.ShortUrls.Create
{
    /// <summary>
    /// Handler class for creating short URLs.
    /// </summary>
    public class CreateShortUrlCommandHandler : IRequestHandler<CreateShortUrlCommand, string>
    {
        /// <summary>
        /// The create short URL repository.
        /// </summary>
        private readonly ICreateShortUrlRepository _createShortUrlRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="createShortUrlRepository">The create short URL repository.</param>
        public CreateShortUrlCommandHandler(ICreateShortUrlRepository createShortUrlRepository)
        {
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
                ShortenedUrl = ShortenUrl(request.Url),
            };

            await _createShortUrlRepository.AddShortUrl(shortUrl);

            return shortUrl.ShortenedUrl;
        }

        /// <summary>
        /// Shortens the given URL.
        /// </summary>
        /// <param name="url">The URL to shorten.</param>
        /// <returns>The shortened URL.</returns>
        private string ShortenUrl(string url)
        {
            return url.Replace("https://www.", string.Empty);
        }
    }
}
