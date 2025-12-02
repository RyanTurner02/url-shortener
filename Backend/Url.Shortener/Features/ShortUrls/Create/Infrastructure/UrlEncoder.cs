using Base62;

namespace Url.Shortener.Features.ShortUrls.Create.Infrastructure
{
    /// <summary>
    /// The URL encoder class.
    /// </summary>
    public static class UrlEncoder
    {
        /// <summary>
        /// Encodes the hashed url using Base62.
        /// </summary>
        /// <param name="hashedUrl">The hashed url.</param>
        /// <returns>The Base62 encoding of the hashed url.</returns>
        public static string Encode(string hashedUrl)
        {
            var base62Converter = new Base62Converter();
            return base62Converter.Encode(hashedUrl);
        }
    }
}
