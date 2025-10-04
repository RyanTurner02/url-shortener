using MediatR;

namespace Backend.Features.ShortUrls.Create
{
    /// <summary>
    /// Endpoint class for creating shortened URLs.
    /// </summary>
    public static class CreateShortUrlEndpoint
    {
        /// <summary>
        /// The route.
        /// </summary>
        private const string ROUTE = "/api/create";

        /// <summary>
        /// Registers the endpoint for creating shortened URLs.
        /// </summary>
        /// <param name="app">The web application.</param>
        public static void RegisterCreateShortUrlEndpoint(this WebApplication app)
        {
            app.MapPost(ROUTE, CreateShortUrl)
                .WithTags("Create Short URL")
                .WithSummary("Creates a shortened URL from a URL.")
                .Produces(StatusCodes.Status201Created);
        }

        /// <summary>
        /// Handles the request for creating shortened URLs.
        /// </summary>
        /// <param name="createShortUrlCommand">The create short URL command.</param>
        /// <param name="sender">The sender.</param>
        /// <returns>The status of creating the short URL.</returns>
        private static async Task<IResult> CreateShortUrl(
            CreateShortUrlCommand createShortUrlCommand,
            ISender sender)
        {
            var shortUrl = await sender.Send(createShortUrlCommand);
            return TypedResults.Created(ROUTE, new ShortUrlResponse(shortUrl));
        }
    }
}
