namespace Url.Shortener.Features.ShortUrls.Create
{
    /// <summary>
    /// The duplicate conflict response.
    /// </summary>
    public class DuplicateConflictResponse
    {
        /// <summary>
        /// The error codename.
        /// </summary>
        public string Error => "DuplicateConflict";

        /// <summary>
        /// The error message.
        /// </summary>
        public string Message => "Failed to generate a unique short URL. Please try again later.";
    }
}
