using MediatR;

namespace Backend.Features.ShortUrls.Get
{
    /// <summary>
    /// Handler class for getting the original URL from a shortened URL.
    /// </summary>
    public class GetShortUrlQueryHandler : IRequestHandler<GetShortUrlQuery, string?>
    {
        /// <summary>
        /// The get short URL repository.
        /// </summary>
        private readonly IGetShortUrlRepository _getShortUrlRepository;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="getShortUrlRepository">The get short URL repository.</param>
        public GetShortUrlQueryHandler(IGetShortUrlRepository getShortUrlRepository)
        {
            _getShortUrlRepository = getShortUrlRepository;
        }

        /// <summary>
        /// Handles the request for getting the original URL from a shortened URL.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The original URL</returns>
        public async Task<string?> Handle(GetShortUrlQuery request, CancellationToken cancellationToken)
        {
            var shortUrl = await _getShortUrlRepository.GetShortUrl(request.ShortUrl);

            return shortUrl?.OriginalUrl;
        }
    }
}
