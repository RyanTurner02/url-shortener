using System.Text;

namespace Url.Shortener.Features.ShortUrls.Create.Utilities
{
    /// <summary>
    /// The URL randomizer class.
    /// </summary>
    public static class UrlRandomizer
    {
        /// <summary>
        /// Retrieves a fixed length of random characters from the hashed url.
        /// </summary>
        /// <param name="length">The length of the </param>
        /// <param name="hashedUrl">The hashed url.</param>
        /// <returns>Random characters from the hashed url.</returns>
        public static string Randomize(int length, string hashedUrl)
        {
            var shortUrl = new StringBuilder();
            var random = new Random();

            while (shortUrl.Length < length)
            {
                var index = random.Next(hashedUrl.Length);
                shortUrl.Append(hashedUrl[index]);
            }

            return shortUrl.ToString();
        }
    }
}
