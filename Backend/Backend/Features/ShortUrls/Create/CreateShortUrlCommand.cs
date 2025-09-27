using MediatR;

namespace Backend.Features.ShortUrls.Create
{
    /// <summary>
    /// The request to create a shortened URL.
    /// </summary>
    /// <param name="Url">The original URL to shorten.</param>
    public record CreateShortUrlCommand(string Url) : IRequest<string>;
}
