using System.Security.Cryptography;
using System.Text;

namespace Url.Shortener.Features.ShortUrls.Create.Infrastructure
{
    /// <summary>
    /// The URL hasher class.
    /// </summary>
    public class UrlHasher : IUrlHasher
    {
        /// <inheritdoc/>
        public string Hash(string originalUrl)
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
