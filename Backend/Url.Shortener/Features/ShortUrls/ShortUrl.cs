namespace Url.Shortener.Features.ShortUrls
{
    /// <summary>
    /// Entity class for storing the mapping between an original URL and its shortened form.
    /// </summary>
    public class ShortUrl
    {
        /// <summary>
        /// Primary key.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The full original url to be shortened.
        /// </summary>
        public required string OriginalUrl { get; set; }

        /// <summary>
        /// The generated shortened url.
        /// </summary>
        public required string ShortenedUrl { get; set; }

        /// <summary>
        /// UTC timestamp when the entry was created.
        /// </summary>
        public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    }
}
