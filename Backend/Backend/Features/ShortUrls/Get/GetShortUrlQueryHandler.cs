using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Backend.Features.ShortUrls.Get
{
    /// <summary>
    /// Handler class for getting the original URL from a shortened URL.
    /// </summary>
    public class GetShortUrlQueryHandler : IRequestHandler<GetShortUrlQuery, string?>
    {
        /// <summary>
        /// The short URL database context.
        /// </summary>
        private readonly ShortUrlDbContext _context;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="context">The short URL database context.</param>
        public GetShortUrlQueryHandler(ShortUrlDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Handles the request for getting the original URL from a shortened URL.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The original URL</returns>
        public async Task<string?> Handle(GetShortUrlQuery request, CancellationToken cancellationToken)
        {
            var shortUrl = await _context.ShortUrls
                .Where(x => x.ShortenedUrl == request.ShortUrl)
                .FirstOrDefaultAsync();

            return shortUrl?.OriginalUrl;
        }
    }
}
