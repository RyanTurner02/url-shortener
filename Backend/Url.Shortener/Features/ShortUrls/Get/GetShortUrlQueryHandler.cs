using MediatR;
using Url.Shortener.Features.ShortUrls.Common;

namespace Url.Shortener.Features.ShortUrls.Get
{
    /// <summary>
    /// Handler class for getting the original URL from a shortened URL.
    /// </summary>
    public class GetShortUrlQueryHandler : IRequestHandler<GetShortUrlQuery, string?>
    {
        /// <summary>
        /// The short URL repository.
        /// </summary>
        private readonly IShortUrlRepository _shortUrlRepository;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="shortUrlRepository">The short URL repository.</param>
        public GetShortUrlQueryHandler(IShortUrlRepository shortUrlRepository)
        {
            _shortUrlRepository = shortUrlRepository;
        }

        /// <summary>
        /// Handles the request for getting the original URL from a shortened URL.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The original URL</returns>
        public async Task<string?> Handle(GetShortUrlQuery request, CancellationToken cancellationToken)
        {
            return await _shortUrlRepository.GetOriginalUrl(request.ShortUrl);
        }
    }
}
