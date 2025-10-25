using Base62;

namespace Url.Shortener.Features.ShortUrls.Create.Infrastructure
{
    /// <summary>
    /// The URL encoder class.
    /// </summary>
    public class UrlEncoder : IUrlEncoder
    {
        /// <inheritdoc/>
        public string Encode(string hashedUrl)
        {
            var base62Converter = new Base62Converter();
            return base62Converter.Encode(hashedUrl);
        }
    }
}
