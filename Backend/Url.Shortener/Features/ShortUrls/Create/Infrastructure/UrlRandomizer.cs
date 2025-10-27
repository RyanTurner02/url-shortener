using System.Text;

namespace Url.Shortener.Features.ShortUrls.Create.Infrastructure
{
    /// <summary>
    /// The URL randomizer class.
    /// </summary>
    public class UrlRandomizer : IUrlRandomizer
    {
        /// <inheritdoc/>
        public string Randomize(int length, string hashedUrl)
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
