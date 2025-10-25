using System.Security.Cryptography;
using System.Text;

namespace Url.Shortener.Features.ShortUrls.Create
{
    /// <summary>
    /// The url shortener service class.
    /// </summary>
    public class UrlShortenerService : IUrlShortenerService
    {
        /// <inheritdoc/>
        public string ShortenUrl(string originalUrl)
        {
            if (string.IsNullOrEmpty(originalUrl))
            {
                return string.Empty;
            }

            var hashedUrl = HashUrl(originalUrl);

            var shortUrlLength = 7;
            return GetRandomCharacters(shortUrlLength, hashedUrl);
        }

        /// <summary>
        /// Hashes the original using the SHA-256 hash function.
        /// </summary>
        /// <param name="originalUrl">The original url.</param>
        /// <returns>The SHA-256 hash of the original url.</returns>
        private string HashUrl(string originalUrl)
        {
            StringBuilder hashedUrl = new StringBuilder();

            using (var sha256 = SHA256.Create())
            {
                var input = Encoding.UTF8.GetBytes(originalUrl);
                var bytes = sha256.ComputeHash(input);

                foreach (var currentByte in bytes)
                {
                    hashedUrl.Append(currentByte.ToString("x2"));
                }
            }

            return hashedUrl.ToString();
        }

        /// <summary>
        /// Retrieves a fixed length of random characters from the hashed url.
        /// </summary>
        /// <param name="length">The length of the </param>
        /// <param name="hashedUrl">The hashed url.</param>
        /// <returns>Random characters from the hashed url.</returns>
        private string GetRandomCharacters(int length, string hashedUrl)
        {
            return string.Empty;
        }
    }
}
