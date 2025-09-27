using MediatR;

namespace Backend.Features.ShortUrls.Get
{
    /// <summary>
    /// Endpoint class for getting shortened URLs.
    /// </summary>
    public static class GetShortUrlEndpoint
    {
        /// <summary>
        /// Registers the endpoint for getting shortened URLs.
        /// </summary>
        /// <param name="app">The web application.</param>
        public static void RegisterGetShortUrlEndpoint(this WebApplication app)
        {
            app.MapGet("/{shortUrl}", GetShortUrl);
        }

        /// <summary>
        /// Handles the request for retriving the original URL from a shortened URL.
        /// </summary>
        /// <param name="shortUrl">The short URL.</param>
        /// <param name="sender">The sender.</param>
        /// <returns>
        /// 302: Redirects to the original URL if found.
        /// 404: If the original URL is not found.
        /// </returns>
        private static async Task<IResult> GetShortUrl(
            string shortUrl,
            ISender sender)
        {
            var originalUrl = await sender.Send(
                new GetShortUrlQuery(shortUrl));

            if (string.IsNullOrEmpty(originalUrl))
            {
                return TypedResults.NotFound();
            }

            return TypedResults.Redirect(originalUrl);
        }
    }
}
