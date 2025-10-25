namespace Url.Shortener.Features.ShortUrls.Create.Infrastructure
{
    /// <summary>
    /// The URL encoder interface.
    /// </summary>
    public interface IUrlEncoder
    {
        /// <summary>
        /// Encodes the hashed url using Base62.
        /// </summary>
        /// <param name="hashedUrl">The hashed url.</param>
        /// <returns>The Base62 encoding of the hashed url.</returns>
        string Encode(string hashedUrl);
    }
}
