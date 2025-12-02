using System.Security.Cryptography;
using System.Text;

namespace Url.Shortener.Features.ShortUrls.Create.Utilities
{
    /// <summary>
    /// The URL hasher class.
    /// </summary>
    public static class UrlHasher
    {
        /// <summary>
        /// Hashes the original URL using the SHA-256 hash function.
        /// </summary>
        /// <param name="originalUrl">The original URL.</param>
        /// <returns>The SHA-256 hash of the original URL.</returns>
        public static string Hash(string originalUrl)
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
    }
}
