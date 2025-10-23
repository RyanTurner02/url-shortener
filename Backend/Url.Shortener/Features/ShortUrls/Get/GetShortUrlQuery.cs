using MediatR;

namespace Url.Shortener.Features.ShortUrls.Get
{
    /// <summary>
    /// The request to get the original URL from a shortened URL.
    /// </summary>
    /// <param name="ShortUrl">The short URL.</param>
    public record GetShortUrlQuery(string ShortUrl) : IRequest<string?>;
}
