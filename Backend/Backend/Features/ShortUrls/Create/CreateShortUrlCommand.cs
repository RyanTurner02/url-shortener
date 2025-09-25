using MediatR;

namespace Backend.Features.ShortUrls.Create
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Url">The URL to shorten</param>
    public record CreateShortUrlCommand(string Url) : IRequest<string>;
}
