namespace Url.Shortener.Features.ShortUrls.Create.Utilities
{
    /// <summary>
    /// The URL randomizer interface.
    /// </summary>
    public interface IUrlRandomizer
    {
        /// <summary>
        /// Retrieves a fixed length of random characters from the hashed url.
        /// </summary>
        /// <param name="length">The length of the </param>
        /// <param name="hashedUrl">The hashed url.</param>
        /// <returns>Random characters from the hashed url.</returns>
        string Randomize(int length, string hashedUrl);
    }
}
