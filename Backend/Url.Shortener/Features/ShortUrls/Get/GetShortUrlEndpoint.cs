using MediatR;

namespace Url.Shortener.Features.ShortUrls.Get
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
            app.MapGet("/{shortUrl}", GetShortUrl)
                .WithTags("Get Short URL")
                .WithSummary("Retrieves the original URL from a short URL.")
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status302Found)
                .Produces(StatusCodes.Status404NotFound);
        }

        /// <summary>
        /// Handles the request for retriving the original URL from a shortened URL.
        /// </summary>
        /// <param name="shortUrl">The short URL.</param>
        /// <param name="sender">The sender.</param>
        /// <returns>
        /// 200: Returns the original URL if found.
        /// 302: Redirects to the original URL if found.
        /// 404: If the original URL is not found.
        /// </returns>
        private static async Task<IResult> GetShortUrl(
            string shortUrl,
            ISender sender,
            HttpRequest req)
        {
            var originalUrl = await sender.Send(
                new GetShortUrlQuery(shortUrl));

            if (string.IsNullOrEmpty(originalUrl))
            {
                return TypedResults.NotFound();
            }

            if (req.Headers["sec-fetch-mode"] == "navigate")
            {
                return TypedResults.Redirect(originalUrl);
            }

            return TypedResults.Ok(new UrlResponse(originalUrl));
        }
    }
}
