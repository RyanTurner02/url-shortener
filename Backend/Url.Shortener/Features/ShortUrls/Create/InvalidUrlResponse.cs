namespace Url.Shortener.Features.ShortUrls.Create
{
    /// <summary>
    /// The invalid URL response.
    /// </summary>
    public class InvalidUrlResponse
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">The error message.</param>
        public InvalidUrlResponse(string message)
        {
            Message = message;
        }

        /// <summary>
        /// The error codename.
        /// </summary>
        public string Error => "InvalidUrl";

        /// <summary>
        /// The error message.
        /// </summary>
        public string Message { get; private set; }
    }
}
